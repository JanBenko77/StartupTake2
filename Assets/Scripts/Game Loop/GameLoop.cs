using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

[System.Serializable]
public enum GameState
{
    PlacingArena,
    DetermineTurnOrder,
    ResetPlayerEnergy,
    CharacterTurns,
    ApplyChanges,
    AfterBattle
}
public class GameLoop : MonoBehaviour
{
    private GameState currentState;
    private Dictionary<GameState, BaseState> stateDictionary = new Dictionary<GameState, BaseState>();

    [SerializeField]
    public TMP_Text InfoText;

    public TMP_Text PlayerHealthText;
    public TMP_Text EnemyHealthText;

    //[SerializeField]
    public List<Character> characters;

    public int playerHealth = 3;
    public int playerEnergy = 10;
    [SerializeField]
    private int playerMaxEnergy = 10;

    public int enemyHealth = 3;
    public int enemyEnergy = 10;
    [SerializeField]
    private int enemyMaxEnergy = 10;



    private void Start()
    {
        stateDictionary.Add(GameState.PlacingArena, new PlacingArenaState(this, GetComponent<PlaceObject>()));
        stateDictionary.Add(GameState.DetermineTurnOrder, new DetermineTurnOrderState(this));
        stateDictionary.Add(GameState.ResetPlayerEnergy, new ResetPlayerEnergyState(this));
        stateDictionary.Add(GameState.CharacterTurns, new CharacterTurnsState(this));
        stateDictionary.Add(GameState.ApplyChanges, new ApplyChangesState(this));
        stateDictionary.Add(GameState.AfterBattle, new AfterBattleState(this));

        currentState = GameState.PlacingArena;
        stateDictionary[currentState].Enter();
        InfoText.text = "Placing arena state";
    }

    public void InitializeCharacters()
    {
        characters = new List<Character>(GameObject.FindObjectsOfType<Character>());
        foreach (Character character in characters)
        {
            character.Initialize(character.characterData);
        }
    }

    private void Update()
    {
        stateDictionary[currentState].Update();
    }

    public void TransitionToState(GameState newState)
    {
        StartCoroutine(TransitionToStateCoroutine(newState));
        InfoText.text = "Transitioning state to " + newState.ToString();
    }

    private IEnumerator TransitionToStateCoroutine(GameState newState)
    {
        stateDictionary[currentState].Exit();

        stateDictionary[currentState].stateIsOver = false;

        yield return StartCoroutine(stateDictionary[newState].EnterCoroutine());

        currentState = newState;

        stateDictionary[currentState].Enter();
    }

    public void IncrementEnergy(int incrementAmount)
    {
        playerMaxEnergy += incrementAmount;
        if (playerMaxEnergy > 15)
        {
            playerMaxEnergy = 15;
        }
        
        enemyMaxEnergy += incrementAmount;
        if (enemyMaxEnergy > 15)
        {
            enemyMaxEnergy = 15;
        }
    }

    public void ResetEnergy()
    {
        playerEnergy = playerMaxEnergy;
        enemyEnergy = enemyMaxEnergy;
    }

    public void TakeDamage(string name)
    {
        if (name == "Player")
        {
            playerHealth -= 1;
            PlayerHealthText.text = "Player health: " + playerHealth.ToString();
            
            if (playerHealth < 1)
            {
                playerHealth = 0;
                TransitionToState(GameState.AfterBattle);
            }
        }
        else if (name == "Enemy")
        {
            enemyHealth -= 1;
            EnemyHealthText.text = "Enemy health: " + enemyHealth.ToString();

            if (enemyHealth < 1)
            {
                enemyHealth = 0;
                TransitionToState(GameState.AfterBattle);
            }
        }
    }
}
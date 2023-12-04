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
    ApplyChanges
}
public class GameLoop : MonoBehaviour
{
    private GameState currentState;
    private Dictionary<GameState, BaseState> stateDictionary = new Dictionary<GameState, BaseState>();

    [SerializeField]
    public TMP_Text InfoText;

    //[SerializeField]
    public List<Character> characters;

    private void Start()
    {
        stateDictionary.Add(GameState.PlacingArena, new PlacingArenaState(this, GetComponent<PlaceObject>()));
        stateDictionary.Add(GameState.DetermineTurnOrder, new DetermineTurnOrderState(this));
        stateDictionary.Add(GameState.ResetPlayerEnergy, new ResetPlayerEnergyState(this));
        stateDictionary.Add(GameState.CharacterTurns, new CharacterTurnsState(this));
        stateDictionary.Add(GameState.ApplyChanges, new ApplyChangesState(this));

        currentState = GameState.PlacingArena;
        stateDictionary[currentState].Enter();
        InfoText.text = "Placing arena state";

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
        InfoText.text = "Transitioning state to" + newState.ToString();
    }

    private IEnumerator TransitionToStateCoroutine(GameState newState)
    {
        stateDictionary[currentState].Exit();
        InfoText.text = "Or this is getting called?";
        yield return StartCoroutine(stateDictionary[newState].EnterCoroutine());

        currentState = newState;

        stateDictionary[currentState].Enter();
        InfoText.text = "This getting called?";
    }
}
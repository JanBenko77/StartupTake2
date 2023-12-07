using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Character))]
public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private Character character;

    private Initialize script;

    private GameLoop gameLoop;

    private void Start()
    {
        character = GetComponent<Character>();
        script = FindObjectOfType<Initialize>();
        gameLoop = FindObjectOfType<GameLoop>();
    }

    public void ChooseAction()
    {

    }

    public List<Character> ChooseRandomTarget()
    {
        var targets = new List<Character>();
        gameLoop.InfoText.text = "Starting to choose random target";
        foreach (Character character in script.characters)
        {
            if (character.characterData.characterType == CharacterType.Player)
            {
                targets.Add(character);
            }
        }
        int randomTarget = Random.Range(0, targets.Count);
        targets.RemoveAt(randomTarget);
        gameLoop.InfoText.text = "Before return is ok";
        return targets;
    }

    public Abilities ChooseRandomAbility()
    {
        int randomAction = Random.Range(0, 3);

        if (randomAction == 0)
        {
            return Abilities.BasicAttack;
        }
        else if (randomAction == 1)
        {
            return this.GetComponent<Character>().Ability1;
        }
        else if (randomAction == 2)
        {
            return this.GetComponent<Character>().Ability2;
        }

        return Abilities.None;
    }
}

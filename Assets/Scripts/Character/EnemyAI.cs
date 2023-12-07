using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Character))]
public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private Character character;

    private GameLoop gameLoop;

    private void Start()
    {
        character = GetComponent<Character>();
        gameLoop = FindObjectOfType<GameLoop>();
    }

    public List<Character> ChooseRandomTarget()
    {
        var targets = new List<Character>();
        foreach (Character character in gameLoop.characters)
        {
            if (character.characterData.characterType == CharacterType.Player)
            {
                targets.Add(character);
            }
        }
        int randomTarget = Random.Range(0, targets.Count);
        targets.RemoveAt(randomTarget);
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

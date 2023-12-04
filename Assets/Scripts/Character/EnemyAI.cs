using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Character character;

    public GameLoop gameLoop;

    private void Start()
    {
        character = GetComponent<Character>();
        gameLoop = FindObjectOfType<GameLoop>();
    }

    public void EnemyChooseRandomAction()
    {       
        ChooseRandomAbility(ChooseRandomTarget());
    }

    private List<Character> ChooseRandomTarget()
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

    private void ChooseRandomAbility(List<Character> targets)
    {
        int randomAction = Random.Range(0, 3);

        if (randomAction == 0)
        {
            character.UseAbility1(targets);
        }
        else if (randomAction == 1)
        {
            character.UseAbility2(targets);
        }
        else if (randomAction == 2)
        {
            character.BasicAttack(targets);
        }
    }
}

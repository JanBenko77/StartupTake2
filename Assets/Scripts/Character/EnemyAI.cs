using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private Character character;
    //[SerializeField]
    //private GameLoop gameLoop;

    private Initialize script;

    private void Start()
    {
        character = GetComponent<Character>();
        //gameLoop = FindObjectOfType<GameLoop>();
        script = FindObjectOfType<Initialize>();
    }

    private void Update()
    {
        PressStuff();
    }

    public void PressStuff()
    {
        if (Input.GetKeyUp(KeyCode.P))
        {
            EnemyChooseRandomAction();
        }
    }

    public void EnemyChooseRandomAction()
    {
        ChooseRandomAbility(ChooseRandomTarget());
    }

    private List<Character> ChooseRandomTarget()
    {
        var targets = new List<Character>();
        foreach (Character character in script.characters)
        {
            if (character.characterData.characterType == CharacterType.Player)
            {
                targets.Add(character);
            }
        }
        int randomTarget = Random.Range(0, targets.Count);
        targets.RemoveAt(randomTarget);
        Debug.Log(targets[0]);
        return targets;
    }

    private void ChooseRandomAbility(List<Character> targets)
    {
        int randomAction = Random.Range(0, 3);

        if (randomAction == 0)
        {
            character.UseAbility1(targets);
            Debug.Log("Ability 1 was used");
        }
        else if (randomAction == 1)
        {
            character.UseAbility2(targets);
            Debug.Log("Ability 2 could have been used lmao");
        }
        else if (randomAction == 2)
        {
            character.BasicAttack(targets);
            Debug.Log("Basic Attack brooooooooooooooooo");
        }
    }
}

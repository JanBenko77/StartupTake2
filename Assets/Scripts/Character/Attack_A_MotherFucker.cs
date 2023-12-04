using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Attack_A_MotherFucker : MonoBehaviour
{
    Character character;

    public Character target;

    List<Character> characterList = new List<Character>();

    public List<Character> targetsUnity = new List<Character>();

    private void Start()
    {
        character = GetComponent<Character>();
        characterList.Add(target);
    }

    private void Update()
    {
        PressShit(characterList);
        PressShift(targetsUnity);
    }

    void PressShit(List<Character> targets)
    {
        if (character != null)
        {
            if(Input.GetKeyUp(KeyCode.Space))
            {
                character.UseAbility1(targets);
                Debug.Log("Attacked guy");
            }    
        }
    }

    void PressShift(List<Character> targets)
    {
        if (character!=null)
        {
            if (Input.GetKeyUp(KeyCode.A))
            {
                character.UseAscension(targets);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Heal_A_MotherFucker : MonoBehaviour
{
    Character character;

    public Character target;

    List<Character> characterList = new List<Character>();

    private void Start()
    {
        character = GetComponent<Character>();
        characterList.Add(target);
    }

    private void Update()
    {
        PressShit(characterList);
    }

    void PressShit(List<Character> targets)
    {
        if (character != null)
        {
            if (Input.GetKeyUp(KeyCode.H))
            {
                character.UseAbility1(targets);
                Debug.Log("Healed a guy");
            }
        }
    }
}

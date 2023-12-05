using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initialize : MonoBehaviour
{
    [SerializeField]
    public List<Character> characters;

    void Start()
    {
        characters = new List<Character>(GameObject.FindObjectsOfType<Character>());
        foreach (Character character in characters)
        {
            character.Initialize(character.characterData);
        }
    }
}

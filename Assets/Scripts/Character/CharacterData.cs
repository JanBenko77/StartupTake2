using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum CharacterType
{
    Player,
    Enemy
}

public enum MythType
{
    Default,
    Japanese,
    Greek
}

[CreateAssetMenu(fileName = "New Character", menuName = "Character")]
public class CharacterData : ScriptableObject
{
    public string characterName;
    public string description;
    public string expansion;
    public MythType mythology;
    public int characterLevel;
    //public RuntimeAnimatorController animatorController;
    
    public int maxHealth;
    public int speed;
    public int attack;
    public int defense;
    public int dodge;
    public int accuracy;

    public Passives Passive;
    public Abilities Ability1;
    public Abilities Ability2;
    public Ascensions Ascension;
    public int ability1Cost;
    public int ability2Cost;

    public CharacterType characterType;
}
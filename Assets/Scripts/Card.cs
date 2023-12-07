using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardType
{
    Default,
    Greek,
    Japanese
}
public enum Passive
{
    Default,
    Never,
    Gonna,
    Give,
    You,
    Up
}
public enum Ability_1
{
    Default,
    Never,
    Gonna,
    Give,
    You,
    Up
}
public enum Ability_2
{
    Default,
    Never,
    Gonna,
    Give,
    You,
    Up
}
public enum Ascension
{
    Default,
    Never,
    Gonna,
    Give,
    You,
    Up
}

public class Card : ScriptableObject
{
    //General card info
    public string characterName;
    public int level;
    [TextArea(15, 20)]
    public string description;
    public CardType mythology;
    public int expansion;

    //Stats
    public int health;
    public int speed;
    public int damage;
    public int defence;
    public int dodge;
    public float hitChance;

    //Abilities
    public Passive passive;
    public Ability_1 ability_1;
    public Ability_2 ability_2;
    public Ascension ascension;

    //Image prefab
    public GameObject prefab;
}

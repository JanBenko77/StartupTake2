using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public CharacterData characterData;

    [SerializeField]
    private string characterName;
    [SerializeField]
    private string description;
    [SerializeField]
    private string expansion;
    [SerializeField]
    private MythType mythology;
    [SerializeField]
    private int characterLevel;
    //public RuntimeAnimatorController animatorController;

    public int maxHealth;
    public int currentHealth;
    public int speed;
    public int attack;
    public int defense;
    public int dodge;
    public int accuracy;

    [SerializeField]
    Passives Passive;
    [SerializeField]
    Abilities Ability1;
    [SerializeField]
    Abilities Ability2;
    [SerializeField]
    Ascensions Ascension;
    [SerializeField]
    int ability1Cost;
    [SerializeField]
    int ability2Cost;

    AbilityScript abilityScript;
    Abilities lastAbilityUsed;
    int lastAbilityUsedCounter;
    public List<Character> target;
    public Character teammate;
    public bool inBattle = false;

    public void Initialize(CharacterData data)
    {
        characterData = data;

        characterName = data.characterName;
        description = data.description;
        expansion = data.expansion;
        mythology = data.mythology;
        characterLevel = data.characterLevel;
        //GetComponent<Animator>().runtimeAnimatorController = data.animatorController;

        maxHealth = data.maxHealth;
        currentHealth = data.maxHealth;
        speed = data.speed;
        attack = data.attack;
        defense = data.defense;
        dodge = data.dodge;
        accuracy = data.accuracy;

        Passive = data.Passive;
        Ability1 = data.Ability1;
        Ability2 = data.Ability2;
        Ascension = data.Ascension;
        ability1Cost = data.ability1Cost;
        ability2Cost = data.ability2Cost;

        abilityScript = FindObjectOfType<AbilityScript>();
        inBattle = true;
    }

    //Here goes the code for checking wether a player has enough mana
    public void BasicAttack(List<Character> targets)
    {
        abilityScript.UseAbility(this, Abilities.BasicAttack, targets, 1);//check this when solved
        lastAbilityUsed = Abilities.BasicAttack;
    }

    public void UseAbility1(List<Character> targets)
    {
        if (lastAbilityUsed == Ability1)
        {
            lastAbilityUsedCounter++;
        }
        else
        {
            lastAbilityUsedCounter = 0;
        }
        abilityScript.UseAbility(this, Ability1, targets, lastAbilityUsedCounter);
        //player.energy -= ability1Cost;
        lastAbilityUsed = Ability1;
        lastAbilityUsedCounter++;
    }

    public void UseAbility2(List<Character> targets)
    {
        if (lastAbilityUsed == Ability2)
        {
            lastAbilityUsedCounter++;
        }
        else
        {
            lastAbilityUsedCounter = 0;
        }
        abilityScript.UseAbility(this, Ability2, targets, lastAbilityUsedCounter);
        //player.energy -= ability2Cost;
        lastAbilityUsed = Ability2;
        lastAbilityUsedCounter++;
    }

    public void UseAscension(List<Character> targets)
    {
        abilityScript.UseAscension(this, Ascension, targets);
        lastAbilityUsed = Abilities.None;
    }

    public Abilities GetAbilityUsed()
    {
        if (Ability1 == Abilities.BasicAttack)
        {
            return Ability1;
        }
        else if (Ability2 == Abilities.BasicAttack)
        {
            return Ability2;
        }
        else
        {
            return Abilities.BasicAttack;
        }
    }

    public void SwitchTarget(Character newTarget)
    {
        target[0] = newTarget;
    }

    public void SkipTurn()
    {
        //Remove out of next turn order
        Debug.Log("Skipped turn for character" + characterName);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Debug.Log("Character " + characterName + " died");
            currentHealth = 0;
            HandleDeathAndSwitch();
        }
    }

    public void HandleDeathAndSwitch()
    {
        inBattle = false;
        foreach (Character character in DetermineTurnOrderState.turnOrder)
        {
            if (character.characterData.characterType == CharacterType.Player && character.inBattle)
            {
                if (character.target[0] == this)
                {
                    if (this.characterData.characterType == CharacterType.Enemy)
                    {
                        FindTeammate();
                        if (this.teammate.inBattle)
                        {
                            character.SwitchTarget(this.teammate);
                        }
                        else
                        {
                            //End the turn early, go to switch state
                        }
                    }
                    else if (this.characterData.characterType == CharacterType.Player)
                    {
                        character.SwitchTarget(character);
                    }
                }
            }

            if (character.characterData.characterType == CharacterType.Enemy && character.inBattle)
            {
                if (character.target[0] == this)
                {
                    if (this.characterData.characterType == CharacterType.Player)
                    {
                        FindTeammate();
                        if (this.teammate.inBattle)
                        {
                            character.SwitchTarget(this.teammate);
                        }
                        else
                        {
                            //End the turn early, go to switch state
                        }
                    }
                    else if (this.characterData.characterType == CharacterType.Enemy)
                    {
                        character.SwitchTarget(character);
                    }
                    
                }
            }
        }
    }

    //Make a method that's going to find this character's teammate

    public void FindTeammate()
    {
        foreach (Character character in DetermineTurnOrderState.turnOrder)
        {
           if (this.characterData.characterType == CharacterType.Player && character.characterData.characterType == CharacterType.Player && character != this)
           {
                this.teammate = character;
           }
           if (this.characterData.characterType == CharacterType.Enemy && character.characterData.characterType == CharacterType.Enemy && character != this)
           {
                this.teammate = character;
           }
        }
    }

    public void SwitchCharacter()
    {

    }




    public void HealDamage(int amount)
    {
        currentHealth += amount;
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
    }

    public void ResetAttack()
    {
        attack = characterData.attack;
    }

    public void ResetDefense()
    {
        defense = characterData.defense;
    }

    public void ResetDodge()
    {
        dodge = characterData.dodge;
    }

    public void ResetAccuracy()
    {
        accuracy = characterData.accuracy;
    }

    public void ResetStats()
    {
        ResetAttack();
        ResetDefense();
        ResetDodge();
        ResetAccuracy();
    }

}
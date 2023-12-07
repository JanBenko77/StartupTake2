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
    public Abilities Ability1;
    [SerializeField]
    public Abilities Ability2;
    [SerializeField]
    public Ascensions Ascension;
    [SerializeField]
    public int ability1Cost;
    [SerializeField]
    public int ability2Cost;

    AbilityScript abilityScript;
    Abilities lastAbilityUsed;
    int lastAbilityUsedCounter;
    public List<Character> target;
    public Character teammate;
    public bool inBattle = false;
    public Animator anim;
    public AudioSource audio;
    public List<AudioClip> audioClip;


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

        anim = GetComponent<Animator>();
        abilityScript = FindObjectOfType<AbilityScript>();
        inBattle = true;
    }

    public void UseAnAbility(Abilities ability, List<Character> target, Ascensions ascension = Ascensions.Default)
    {
        GameLoop gameloop = FindObjectOfType<GameLoop>();
        if (ability == Ability1)
        {
            UseAbility1(target);
        }
        else if (ability == Ability2)
        {
            UseAbility2(target);
        }
        else
        {
            BasicAttack(target);
        }
        if (ascension == Ascensions.Medusa)
        {
            UseAscension(target);
        }
    }

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
        if (Ascension != Ascensions.Default)
        {
            abilityScript.UseAscension(this, Ascension, targets);
            lastAbilityUsed = Abilities.None;
        }
        else
        {
            //Nothing
        }
    }

    public Abilities GetAbilityUsed()
    {
        if (lastAbilityUsed == Ability1)
        {
            return Ability1;
        }
        else if (lastAbilityUsed == Ability2)
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
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            anim.SetBool("Die", true);
            HandleDeathAndSwitch();
        }
    }

    public void HandleDeathAndSwitch()
    {
        GameLoop gameLoop = FindObjectOfType<GameLoop>();
        if (this.characterData.characterType == CharacterType.Enemy)
        {
            gameLoop.TakeDamage("Enemy");
        }
        else
        {
            gameLoop.TakeDamage("Player");
        }
        ResetHealth();
        /*foreach (Character character in DetermineTurnOrderState.turnOrder)
        {
            if (character.characterData.characterType == CharacterType.Player && character.inBattle)
            {
                gameLoop.InfoText.text = "Breaks at 1";
                if (character.target[0] == this)
                {
                    gameLoop.InfoText.text = "Breaks at 2";
                    if (this.characterData.characterType == CharacterType.Enemy)
                    {
                        gameLoop.InfoText.text = "Breaks at 3";
                        FindTeammate();
                        gameLoop.InfoText.text = "Breaks at 4";
                        if (this.teammate.inBattle)
                        {
                            gameLoop.InfoText.text = "Breaks at 5";
                            character.SwitchTarget(this.teammate);
                            gameLoop.InfoText.text = "Breaks at 6";
                        }
                        else
                        {
                            //End the turn early, go to switch state
                        }
                    }
                    else if (this.characterData.characterType == CharacterType.Player)
                    {
                        character.SwitchTarget(character);
                        gameLoop.InfoText.text = "Breaks at 7";
                    }
                }
            }

            if (character.characterData.characterType == CharacterType.Enemy && character.inBattle)
            {
                gameLoop.InfoText.text = "Breaks at 8";
                if (character.target[0] == this)
                {
                    gameLoop.InfoText.text = "Breaks at 9";
                    if (this.characterData.characterType == CharacterType.Player)
                    {
                        gameLoop.InfoText.text = "Breaks at 10";
                        FindTeammate();
                        gameLoop.InfoText.text = "Breaks at 11";
                        if (this.teammate.inBattle)
                        {
                            gameLoop.InfoText.text = "Breaks at 12";
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
                        gameLoop.InfoText.text = "Breaks at 13";
                    }
                    
                }
            }
        }*/
    }

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

    private void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Animation Ended"))
        {
            anim.SetInteger("Attack", 0);
            anim.SetBool("Animation Ended", true);
        }
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            anim.SetBool("Animation Ended", false);
        }
    }
}

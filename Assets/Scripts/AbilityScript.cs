using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Passives
{
    Default
}

public enum Abilities
{
    None,
    BasicAttack,
    DoubleAttack,
    HalfDefense,
    Block,
    BuffDefense,
    Intercept,
    QuickAction,
    Heal,
    Invlunerable,
    ForceChange,
    StunAttack
}

public enum Ascensions
{
    Default,
    Medusa
}


public class AbilityScript : MonoBehaviour
{
    public void UseAbility(Character character, Abilities ability, List<Character> targets, int turnUseCounter)
    {
        switch (ability)
        {
            case Abilities.BasicAttack:
                BasicAttack(character, targets);
                break;

            case Abilities.DoubleAttack:
                DoubleAttack(character, targets);
                break;

            case Abilities.HalfDefense:
                HalfDefense(targets);
                break;

            case Abilities.Block:
                break;

            case Abilities.BuffDefense:
                break;

            case Abilities.Intercept:
                break;

            case Abilities.QuickAction:
                break;

            case Abilities.Heal:
                Heal(character, targets, 0);
                break;

            case Abilities.Invlunerable:
                break;

            case Abilities.ForceChange:
                break;
                
            case Abilities.StunAttack:
                StunAttack(character, targets, turnUseCounter);
                break;

            default:
                break;
        }
    }

    private void BasicAttack(Character character, List<Character> targets)
    {
        if (HitCheck(character, targets)[0])
        {
            targets[0].TakeDamage(character.attack - targets[0].defense);
        }
        else
        {
            Debug.Log("Missed");
        }
    }

    private void DoubleAttack(Character character, List<Character> targets)
    {
        character.attack *= 2;
        BasicAttack(character, targets);
        character.attack /= 2;
        Debug.Log("Activated Double Attack");
    }

    public void HalfDefense(List<Character> targets)
    {
        foreach (Character target in targets)
        {
            target.defense /= 2;
        }
    }

    public void Block(Character defender, Character attacker, Character teammate)//this should be checked before each attack
    {
        if (attacker.GetAbilityUsed() == Abilities.BasicAttack)
        {
            if (attacker.target[0] == teammate)
            {
                defender.SwitchTarget(defender);
            }
            else
            {
                Debug.Log("Skill issue, L bozo, get fucked");
            }
        }
        if (attacker.GetAbilityUsed() == Abilities.DoubleAttack)
        {
            if (attacker.target[0] == teammate)
            {
                defender.SwitchTarget(defender);
            }
            else
            {
                Debug.Log("Skill issue, L bozo, get fucked");
            }
        }
        if (attacker.GetAbilityUsed() == Abilities.HalfDefense)
        {
            if (attacker.target[0] == teammate)
            {
                defender.SwitchTarget(defender);
            }
            else
            {
                Debug.Log("Skill issue, L bozo, get fucked");
            }
        }
    }

    private void StunAttack(Character character, List<Character> targets, int hitChanceIncrease)
    {
        if (HitCheck(character, targets, hitChanceIncrease, 20)[0])
        {
            targets[0].TakeDamage(character.attack - targets[0].defense);
            targets[0].SkipTurn();
        }
        else
        {
            Debug.Log("Missed");
        }
    }

    private void Heal(Character character, List<Character> targets, int healAmount)
    {
        if (BuffCheck(character, targets)[0])
        {
            targets[0].HealDamage(30);
            if (targets[0].currentHealth > targets[0].maxHealth)
            {
                targets[0].currentHealth = targets[0].maxHealth;
            }
        }

    }



    private List<bool> HitCheck(Character character, List<Character> targets, int hitChance = 0, int multiplier = 0)
    {
        List<bool> results = new List<bool>();

        for (int i = 0; i < targets.Count; i++)
        {
            if (Random.Range(0, 100) <= character.accuracy - targets[i].dodge - (hitChance * multiplier))
            {
                results.Add(true);
            }
            else
            {
                results.Add(false);
            }       
        }
        return results;
    }

    private List<bool> BuffCheck(Character character, List<Character> targets, int hitChance = 0, int multiplier = 0)
    {
        List<bool> results = new List<bool>();

        for (int i = 0; i < targets.Count; i++)
        {
            if (Random.Range(0, 100) <= character.accuracy - (hitChance * multiplier))
            {
                results.Add(true);
            }
            else
            {
                results.Add(false);
            }
        }
        return results;
    }


    public void UseAscension(Character character, Ascensions ascensions, List<Character> targets)
    {
        switch(ascensions)
        {
            case Ascensions.Medusa:
                MedusaAscension(character, targets);
                break;
        }
    }

    public void MedusaAscension(Character character, List<Character> targets)
    {
        AscensionCheck(character, targets);
    }

    private void AscensionCheck(Character character, List<Character> targets)
    {
        foreach(Character target in targets)
        {
            target.SkipTurn();
        }
    }
}

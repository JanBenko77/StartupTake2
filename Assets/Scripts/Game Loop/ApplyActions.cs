using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ApplyActions : MonoBehaviour
{
    private GameLoop gameLoop;

    private SelectAction actionSelector;

    private List<Character> turnOrder = new List<Character>();

    public bool actionsExecuted = false;

    private void Awake()
    {
        gameLoop = FindObjectOfType<GameLoop>();

        actionSelector = FindObjectOfType<SelectAction>();

        this.enabled = false;
    }

    private void OnEnable()
    {
        turnOrder = DetermineTurnOrderState.turnOrder;
    }

    private void OnDisable()
    {
        turnOrder.Clear();
        actionsExecuted = false;
    }

    public IEnumerator ExecuteActions()
    {
        foreach (Character character in turnOrder)
        {
            if (character == actionSelector.character1)
            {
                if (actionSelector.selectedAscensionCharacter1 == Ascensions.Medusa)
                {
                    yield return (character.UseAscension(actionSelector.selectedTargetCharacter1));
                }
                else
                {
                    yield return (character.UseAnAbility(actionSelector.selectedActionCharacter1, actionSelector.selectedTargetCharacter1));
                }
            }
            else if (character == actionSelector.character2)
            {
                if (actionSelector.selectedAscensionsCharacter2 == Ascensions.Medusa)
                {
                    yield return (character.UseAscension(actionSelector.selectedTargetCharacter2));
                }
                else
                {
                    yield return (character.UseAnAbility(actionSelector.selectedActionCharacter2, actionSelector.selectedTargetCharacter2));
                }
            }
            else if (character == actionSelector.enemy2)
            {
                if (actionSelector.selectedAscensionEnemy2 == Ascensions.Medusa)
                {
                    yield return (character.UseAscension(actionSelector.selectedTargetEnemy2));
                }
                else
                {
                    yield return (character.UseAnAbility(actionSelector.selectedActionEnemy2, actionSelector.selectedTargetEnemy2));
                }
            }
            else if (character == actionSelector.enemy1)
            {
                if (actionSelector.selectedAscensionEnemy1 == Ascensions.Medusa)
                {
                    yield return (character.UseAscension(actionSelector.selectedTargetEnemy1));
                }
                else
                {
                    yield return (character.UseAnAbility(actionSelector.selectedActionEnemy1, actionSelector.selectedTargetEnemy1));
                }
            }
            if (!character.inBattle)
            {
                if (character.characterData.characterType == CharacterType.Player)
                {
                    gameLoop.TakeDamage("Player");
                }
                else
                {
                    gameLoop.TakeDamage("Enemy");
                }
            }
        }
        actionsExecuted = true;
        yield break;
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class ApplyActions : MonoBehaviour
{
    private GameLoop gameLoop;

    private SelectAction actionSelector;

    private List<Character> turnOrder = new List<Character>();

    public bool actionsExecuted = false;
    public TMP_Text InfoText;
    private void Awake()
    {
        gameLoop = FindObjectOfType<GameLoop>();
        foreach (TMP_Text textbox in FindObjectsOfType<TMP_Text>())
        {
            if (textbox.name == "DebugText") { InfoText = textbox; break; }
        }
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
        gameLoop.DebugText.text = "Try ExecuteActions";
        while (!actionsExecuted)
        {
            gameLoop.DebugText.text = "ExecuteActions";
            foreach (Character character in turnOrder)
            {
                gameLoop.DebugText.text = "Foreach Character";
                if (character == actionSelector.character1)
                {
                    gameLoop.DebugText.text = "If character = character1";
                    if (actionSelector.selectedAscensionCharacter1 == Ascensions.Medusa)
                    {
                        gameLoop.DebugText.text = "If attack selected ascension";
                        yield return  StartCoroutine(character.UseAscension(actionSelector.selectedTargetCharacter1));

                    }
                    else
                    {
                        gameLoop.DebugText.text = "If attack selected normal";
                        yield return  StartCoroutine(character.UseAnAbility(actionSelector.selectedActionCharacter1, actionSelector.selectedTargetCharacter1));
                    }
                }
                else if (character == actionSelector.character2)
                {
                    gameLoop.DebugText.text = "If character = character2";
                    if (actionSelector.selectedAscensionsCharacter2 == Ascensions.Medusa)
                    {
                        gameLoop.DebugText.text = "If attack selected ascension 2";
                        yield return StartCoroutine(character.UseAscension(actionSelector.selectedTargetCharacter2));
                    }
                    else
                    {
                        gameLoop.DebugText.text = "If attack selected normal 2";
                        yield return StartCoroutine(character.UseAnAbility(actionSelector.selectedActionCharacter2, actionSelector.selectedTargetCharacter2));
                    }
                }
                else if (character == actionSelector.enemy2)
                {
                    gameLoop.DebugText.text = "If character = character3";
                    if (actionSelector.selectedAscensionEnemy2 == Ascensions.Medusa)
                    {
                        gameLoop.DebugText.text = "If attack selected ascension 3";
                        yield return StartCoroutine(character.UseAscension(actionSelector.selectedTargetEnemy2));
                    }
                    else
                    {
                        gameLoop.DebugText.text = "If attack selected normal 3";
                        yield return StartCoroutine(character.UseAnAbility(actionSelector.selectedActionEnemy2, actionSelector.selectedTargetEnemy2));
                    }
                }
                else if (character == actionSelector.enemy1)
                {
                    gameLoop.DebugText.text = "If character = character4";
                    if (actionSelector.selectedAscensionEnemy1 == Ascensions.Medusa)
                    {
                        gameLoop.DebugText.text = "If attack selected ascension 4";
                        yield return StartCoroutine(character.UseAscension(actionSelector.selectedTargetEnemy1));
                    }
                    else
                    {
                        gameLoop.DebugText.text = "If attack selected normal 4";
                        yield return StartCoroutine(character.UseAnAbility(actionSelector.selectedActionEnemy1, actionSelector.selectedTargetEnemy1));
                    }
                }
            }
            if (!turnOrder[0].inBattle)
            {
                gameLoop.DebugText.text = "WOW I got through everything";
                if (turnOrder[0].characterData.characterType == CharacterType.Player)
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






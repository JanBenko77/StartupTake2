using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Remove the character from the TurnOrder arraylist if they were KO'd, and remove the character from the turn order that just took their turn, set them to !InBattle
//If a character was KO'd, switch for a different character, can't be the same character that just got KO'd, reset that character's stats(HP) to max
//If there are no more characters in the TurnOrder arraylist, go back to DetermineTurnOrder

public class ApplyChangesState : BaseState
{
    private GameLoop gameLoop;

    private ApplyActions applyActionsScript;

    public ApplyChangesState(GameLoop loop)
    {
        gameLoop = loop;

        applyActionsScript = FindObjectOfType<ApplyActions>();
    }

    override public void Enter()
    {
        applyActionsScript.enabled = true;
        gameLoop.StartCoroutine(TransitionCoroutine());
    }

    override public IEnumerator EnterCoroutine()
    {
        yield return null;
    }

    private IEnumerator TransitionCoroutine()
    {

        yield return new WaitForSeconds(2.0f);
    }

    override public void Update()
    {
        
    }

    override public void Exit()
    {
        
    }

    bool ConditionMet()
    {
        return true;
    }
}
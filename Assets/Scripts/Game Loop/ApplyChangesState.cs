using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
        applyActionsScript.ExecuteActions();
        stateIsOver = false;

        gameLoop.StartCoroutine(EnterCoroutine());
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
        if (ConditionMet() && !stateIsOver)
        {
            gameLoop.TransitionToState(GameState.DetermineTurnOrder);
        }
    }

    override public void Exit()
    {
        stateIsOver = true;
        applyActionsScript.enabled = false;
        gameLoop.StartCoroutine(TransitionCoroutine());
    }

    bool ConditionMet()
    {
        if (applyActionsScript.actionsExecuted)
        {
            gameLoop.InfoText.text = "Actions executed";
            return true;
        }
        return false;
    }
}
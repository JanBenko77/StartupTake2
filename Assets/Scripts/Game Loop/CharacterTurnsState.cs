using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Allow player input to choose an action for each character
//Enemy chooses random action for each character
//Check if a character can use an ability

public class CharacterTurnsState : BaseState
{
    private GameLoop gameLoop;

    private SelectAction actionSelector;

    bool stateIsOver = false;

    public CharacterTurnsState(GameLoop loop)
    {
        gameLoop = loop;

        actionSelector = gameLoop.GetComponent<SelectAction>();
    }

    override public void Enter()
    {
        gameLoop.InfoText.text = "Select character turns";
        actionSelector.enabled = true;
        gameLoop.StartCoroutine(EnterCoroutine());
    }

    override public IEnumerator EnterCoroutine()
    {
        yield return null;
    }

    private IEnumerator TransitionCoroutine()
    {
        gameLoop.InfoText.text = "Starting transition into thing";

        yield return new WaitForSeconds(2.0f);
    }

    override public void Update()
    {
        if (ConditionMet() && !stateIsOver)
        {
            gameLoop.TransitionToState(GameState.ApplyChanges);
        }
    }

    override public void Exit()
    {
        stateIsOver = true;
        actionSelector.ResetActions();
        gameLoop.StartCoroutine(TransitionCoroutine());
    }

    bool ConditionMet()
    {
        if (actionSelector.bothActionsSelected)//This should be SelectAction.actionsSelected
        {
            gameLoop.InfoText.text = "Turn order determined";
            actionSelector.enabled = false;
            return true;
        }
        return false;
    }
}

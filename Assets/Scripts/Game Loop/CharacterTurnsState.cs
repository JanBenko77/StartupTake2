using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Allow player input to choose an action for each character
//Enemy chooses random action for each character
//Check if a character can use an ability

public class CharacterTurnsState : BaseState
{
    private GameLoop gameLoop;

    bool stateIsOver = false;

    public CharacterTurnsState(GameLoop loop)
    {
        gameLoop = loop;
    }

    override public void Enter()
    {
        gameLoop.InfoText.text = "Resetting player energy";
        gameLoop.StartCoroutine(EnterCoroutine());
        //Change UI to allow player to choose an action for each character
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
        gameLoop.StartCoroutine(TransitionCoroutine());
    }

    bool ConditionMet()
    {
        if (stateIsOver)//This should be SelectAction.actionsSelected
        {
            gameLoop.InfoText.text = "Turn order determined";
            return true;
        }
        return false;
    }
}

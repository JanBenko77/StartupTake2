using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Have an <ArrayList>TurnOrder of all inBattle characters
//Take the speed of each character and add it to the arraylist
//Sort the arraylist from highest to lowest
//At the end check if there was a tie
//If there was a tie, have the characters randomly choose who goes first
//If a Quick Action was used, place that character at the front of the arraylist

public class DetermineTurnOrderState : BaseState
{
    private GameLoop gameLoop;

    public DetermineTurnOrderState(GameLoop loop)
    {
        gameLoop = loop;
    }

    override public void Enter()
    {
        gameLoop.StartCoroutine(TransitionCoroutine());
    }

    override public IEnumerator EnterCoroutine()
    {
        yield return null;
    }

    private IEnumerator TransitionCoroutine()
    {
        yield return new WaitForSeconds(2.0f);

        if (ConditionMet())
        {
            gameLoop.TransitionToState(GameState.ResetPlayerEnergy);
        }
    }

    override public void Update()
    {
        //Update thing here
    }

    override public void Exit()
    {
        //Exiting stuff
    }

    bool ConditionMet()
    {
        return false;
    }
}

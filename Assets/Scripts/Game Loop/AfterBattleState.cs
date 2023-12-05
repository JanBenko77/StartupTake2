using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Check if there are any !inBattle characters in the turnOrder?
//For each dead character, switch another character in
//Switch to DetermineOrderState
//Reset stats for all characters in turnOrder

public class AfterBattleState : BaseState
{
    private GameLoop gameLoop;

    public AfterBattleState(GameLoop loop)
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
            gameLoop.TransitionToState(GameState.CharacterTurns);
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
        return true;
    }
}

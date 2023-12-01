using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Reset all player energy to MaxEnergy
//Maybe increment max energy / up to designers

public class ResetPlayerEnergyState : BaseState
{
    private GameLoop gameLoop;

    public ResetPlayerEnergyState(GameLoop loop)
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
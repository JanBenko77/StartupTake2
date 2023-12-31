using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPlayerEnergyState : BaseState
{
    private GameLoop gameLoop;

    bool energyReset = false;

    public ResetPlayerEnergyState(GameLoop loop)
    {
        gameLoop = loop;
    }

    override public void Enter()
    {
        gameLoop.InfoText.text = "Resetting player energy";
        gameLoop.StartCoroutine(EnterCoroutine());
        //gameLoop.IncrementEnergy(2);  //Turn this on to increment energy each turn
        gameLoop.ResetEnergy();
        stateIsOver = false;
        energyReset = true;
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
            gameLoop.TransitionToState(GameState.CharacterTurns);
        }
    }

    override public void Exit()
    {
        stateIsOver = true;
        gameLoop.StartCoroutine(TransitionCoroutine());
    }

    bool ConditionMet()
    {
        if (energyReset)
        {
            gameLoop.InfoText.text = "Reset energy";
            return true;
        }
        return false;
    }
}

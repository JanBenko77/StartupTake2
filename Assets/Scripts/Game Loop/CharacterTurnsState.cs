using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Allow player input to choose an action for each character
//Enemy chooses random action for each character
//Check if a character can use an ability

public class CharacterTurnsState : BaseState
{
    private GameLoop gameLoop;

    public CharacterTurnsState(GameLoop loop)
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
            gameLoop.TransitionToState(GameState.ApplyChanges);
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

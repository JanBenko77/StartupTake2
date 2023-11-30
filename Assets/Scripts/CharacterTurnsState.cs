using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//From the TurnOrder arraylist, take the first character and have them take their turn
//Set the character to active
//Allow the player input for that character
//Change player UI - display character stats, abilities, etc.
//When a player clicks on an action, have them select a target if needed
//When the player confirms their action, go to ApplyChanges

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

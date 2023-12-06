using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//If a character died, remove them from the TurnOrder arraylist
//Subtract the energy cost of the action from the character
//Apply the action to target(s) - de/buff, start a timer for certain amount of turns, etc.
//Apply the action to the target(s) - damage, healing
//Player HP takes damage if a character was KO'd
//If player HP reaches 0, go to GameOver, else continue
//Remove the character from the TurnOrder arraylist if they were KO'd, and remove the character from the turn order that just took their turn, set them to !InBattle
//If a character was KO'd, switch for a different character, can't be the same character that just got KO'd, reset that character's stats(HP) to max
//If there are no more characters in the TurnOrder arraylist, go back to DetermineTurnOrder

public class ApplyChangesState : BaseState
{
    private GameLoop gameLoop;

    

    public ApplyChangesState(GameLoop loop)
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
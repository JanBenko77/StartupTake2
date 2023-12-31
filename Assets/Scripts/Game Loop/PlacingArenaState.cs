using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Player taps on the arena to confirm placement
//Stop detecting surfaces
//Hide all detected surfaces, leave arena visible
//Place selected characters on specified spots on the arena
//Place enemy characters on specified spots on the arena
public class PlacingArenaState : BaseState
{
    private GameLoop gameLoop;
    private PlaceObject arenaPlacer;

    public PlacingArenaState(GameLoop loop, PlaceObject arenaPlacerRef)
    {
        gameLoop = loop;
        arenaPlacer = arenaPlacerRef;
    }

    override public void Enter()
    {
        gameLoop.StartCoroutine(EnterCoroutine());
        arenaPlacer.enabled = true;
        stateIsOver = false;
    }

    override public IEnumerator EnterCoroutine()
    {
        yield return null;
    }

    private IEnumerator TransitionCoroutine()
    {
        gameLoop.InfoText.text = "Starting transition into thing";
        Debug.Log("gameLoop.InfoText.text = '" + gameLoop.InfoText.text + "'");
        yield return new WaitForSecondsRealtime(2.0f);
        //gameLoop.transitionIsOver = true;
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
        arenaPlacer.enabled = false;
        gameLoop.StartCoroutine(TransitionCoroutine());
    }

    bool ConditionMet() //condition is if the player placed the arena and it's confirmed
    {
        if (arenaPlacer.isPlaced && !stateIsOver)
        {
            gameLoop.InitializeCharacters();
            gameLoop.InfoText.text = "Arena placed";
            return true;
        }
        return false;
    }
}
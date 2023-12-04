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
    private bool isOver = false;

    public PlacingArenaState(GameLoop loop, PlaceObject arenaPlacerRef)
    {
        gameLoop = loop;
        arenaPlacer = arenaPlacerRef;
    }

    override public void Enter()
    {
        gameLoop.StartCoroutine(EnterCoroutine());
        arenaPlacer.enabled = true;
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
        if (ConditionMet() && !isOver)
        {
            gameLoop.TransitionToState(GameState.DetermineTurnOrder);
        }
    }

    override public void Exit()
    {
        isOver = true;
        arenaPlacer.enabled = false;
        gameLoop.StartCoroutine(TransitionCoroutine());
    }

    bool ConditionMet() //condition is if the player placed the arena and it's confirmed
    {
        if (arenaPlacer.isPlaced && !isOver)
        {
            gameLoop.InfoText.text = "Arena placed";
            return true;
        }
        return false;
    }
}
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DetermineTurnOrderState : BaseState
{
    private GameLoop gameLoop;

    public static List<Character> turnOrder = new List<Character>();

    bool stateIsOver = false;

    bool orderDetermined = false;

    public DetermineTurnOrderState(GameLoop loop)
    {
        gameLoop = loop;
    }

    override public void Enter()
    {
        gameLoop.InfoText.text = "Determining turn order";
        gameLoop.StartCoroutine(EnterCoroutine());
        turnOrder.Clear();
        foreach (Character character in gameLoop.characters)
        {
            if (character.inBattle)
            {
                turnOrder.Add(character);
            }
        }
        turnOrder = turnOrder.OrderByDescending(c => c.characterData.speed).ToList();
        orderDetermined = true;
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
            gameLoop.TransitionToState(GameState.DetermineTurnOrder);
        }
    }

    override public void Exit()
    {
        stateIsOver = true;
        gameLoop.StartCoroutine(TransitionCoroutine());
    }

    bool ConditionMet() //condition is if the turn order is determined
    {
        if (orderDetermined)
        {
            gameLoop.InfoText.text = "Turn order determined";
            return true;
        }
        return false;
    }
}

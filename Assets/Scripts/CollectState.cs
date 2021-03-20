using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectState : State
{
    public State DoState(Player player)
    {
        GameObject gm = GameObject.Find("GameManager");
        player.renderer.sharedMaterial = gm.GetComponent<GameManager>().collectColor;

        if (!gm.GetComponent<GameManager>().playStatus)
        {
            return player.endGameState;
        }

        player.Move();

        if (player.foundPredator || player.foundPrey)
        {
            if (player.foundPrey)
                return player.killState;
            else
                return player.runState;
        }
        else if (player.foundCoin)
        {
            return player.collectState;
        }
        else
        {
            player.targetObject = null;
            return player.idleState;
        }
    }
}

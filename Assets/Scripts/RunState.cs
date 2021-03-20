using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : State
{
    public State DoState(Player player)
    {
        GameObject gm = GameObject.Find("GameManager");
        player.renderer.sharedMaterial = gm.GetComponent<GameManager>().runColor;

        if (!gm.GetComponent<GameManager>().playStatus)
        {
            return player.endGameState;
        }

        player.Move();

        if (player.foundPredator)
        {
            return player.runState;
        }
        else
        {
            return player.idleState;
        }
    }
}

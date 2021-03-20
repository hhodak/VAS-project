using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillState : State
{
    public State DoState(Player player)
    {
        GameObject gm = GameObject.Find("GameManager");
        player.renderer.sharedMaterial = gm.GetComponent<GameManager>().killColor;

        if (!gm.GetComponent<GameManager>().playStatus)
        {
            return player.endGameState;
        }

        if (player.foundPrey)
        {
            player.currentSpeed = player.boostSpeed;
            player.Follow();
            return player.killState;
        }
        else
        {
            player.targetObject = null;
            player.currentSpeed = player.normalSpeed;
            return player.idleState;
        }
    }
}

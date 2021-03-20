using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameState : State
{
    public State DoState(Player player)
    {
        GameObject gm = GameObject.Find("GameManager");
        player.renderer.sharedMaterial = gm.GetComponent<GameManager>().endGameColor;

        player.GetComponent<Rigidbody>().velocity = Vector3.zero;

        return player.endGameState;
    }
}

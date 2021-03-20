using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    public State DoState(Player player)
    {
        GameObject gm = GameObject.Find("GameManager");
        player.renderer.sharedMaterial = gm.GetComponent<GameManager>().idleColor;

        if (!gm.GetComponent<GameManager>().playStatus)
        {
            return player.endGameState;
        }

        Idling(gm.GetComponent<GameManager>(), player);

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
            return player.idleState;
        }
    }

    void Idling(GameManager gm, Player player)
    {
        if (player.arrived)
        {
            DefineNewTargetPosition(gm, player);
        }

        player.Move();

        if (Vector3.Distance(player.transform.position, player.targetPosition) < 0.5f)
        {
            player.arrived = true;
        }
    }

    void DefineNewTargetPosition(GameManager gm, Player player)
    {
        float posX = Random.Range(-gm.fieldSize, gm.fieldSize);
        float posZ = Random.Range(-gm.fieldSize, gm.fieldSize);

        Vector3 vctr = new Vector3(posX, 0, posZ);
        player.targetPosition = vctr;

        player.arrived = false;
    }
}

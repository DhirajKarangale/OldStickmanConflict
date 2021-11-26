using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public float[] playerPos;
    public float health;

    public PlayerData(PlayerHealth player)
    {
        health = player.currHealth;

        playerPos = new float[3];
        playerPos[0] = player.transform.position.x;
        playerPos[2] = player.transform.position.y;
        playerPos[1] = player.transform.position.z;
    }
}

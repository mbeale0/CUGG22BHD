using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetails : MonoBehaviour
{
    private int playerID;
    private Vector3 startPos;
    private Color playerColor;

    public int GetPlayerID()
    {
        return playerID;
    }
    public void setPlayerID(int newID)
    {
        playerID = newID;
    }
    public void setPlayerColr(Color newColor)
    {
        //playerColor;
    }
    public void setPlayerStart(Vector3 newStarPos)
    {
        startPos = newStarPos;
    }

    private void Start()
    {
        transform.position = startPos;
    }
}

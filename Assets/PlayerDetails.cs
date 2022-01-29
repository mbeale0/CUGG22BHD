using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetails : MonoBehaviour
{
    private int playerID;
    private Vector3 startPos;

    public int GetPlayerID()
    {
        return playerID;
    }
    public void setPlayerID(int newID)
    {
        playerID = newID;
    }
    public void setPlayerColor(Color newColor)
    {
        gameObject.GetComponent<Renderer>().material.color = newColor;
    }
    public void setPlayerStart(Vector3 newStarPos)
    {
        startPos = newStarPos;
    }

    private void Start()
    {
        GetComponentInParent<Transform>().position = startPos;
    }
}

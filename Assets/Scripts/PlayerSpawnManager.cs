using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] spawnLocations = null;

    public void OnPlayerJoined(PlayerInput playerInput)
    {
        /*Debug.Log("player joined with ID: " + playerInput.playerIndex);

        playerInput.gameObject.GetComponent<PlayerDetails>().setPlayerID(playerInput.playerIndex + 1);

        playerInput.gameObject.GetComponent<PlayerDetails>().setPlayerStart(spawnLocations[playerInput.playerIndex].transform.position);

        if (playerInput.playerIndex == 0)
        {
            playerInput.gameObject.GetComponent<PlayerDetails>().setPlayerColor(Color.red);
        }
        else
        {
            playerInput.gameObject.GetComponent<PlayerDetails>().setPlayerColor(Color.blue);
        }*/
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelection : MonoBehaviour
{
    private int playerOneCharacter;
    private int playerTwoCharacter;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    public int GetOneCharacter()
    {
        return playerOneCharacter;
    }
    public int GetTwoCharacter()
    {
        return playerTwoCharacter;
    }
    public void SetOneCharacter(int playerCharacter)
    {
        playerOneCharacter = playerCharacter;
    }
    public void SetTwoCharacter(int playerCharacter)
    {
        playerTwoCharacter = playerCharacter;
    }
    
}

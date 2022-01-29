using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private Text playerOneScoreText = null;
    [SerializeField] private Text playerTwoScoreText = null;

    private int scoreOneText = 0;
    private int scoreTwoText = 0;

    private void Start()
    {
        if(playerOneScoreText != null)
        {
            playerOneScoreText.text = "Player One: 0";
        }
        else if(playerTwoScoreText != null)
        {
            playerTwoScoreText.text = "Player Two: 0";
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("PlayerOne"))
        {
            scoreOneText++;
            playerOneScoreText.text = $"Player One: {scoreOneText}";
            if(scoreOneText >= 3)
            {
                Victory("One");
            }
            other.gameObject.GetComponent<Controls>().ResetToStartPos();
            GameObject.FindGameObjectWithTag("PlayerTwo").GetComponent<Controls>().ResetToStartPos();
        }
        else if (other.CompareTag("PlayerTwo"))
        {
            scoreTwoText++;
            playerTwoScoreText.text = $"Player Two: {scoreTwoText}";
            if(scoreTwoText >= 3)
            {
                Victory("Two");
            }
            other.gameObject.GetComponent<Controls>().ResetToStartPos();
            GameObject.FindGameObjectWithTag("PlayerOne").GetComponent<Controls>().ResetToStartPos();
        }

        
    }

    private void Victory(string victoriousPlayer)
    {
        Debug.Log($"Player {victoriousPlayer} won!");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private Text playerOneScoreText = null;
    [SerializeField] private Text playerTwoScoreText = null;
    [SerializeField] private GameObject WinCanvas = null;
    [SerializeField] private Text WinText = null;

    private int scoreOneText = 0;
    private int scoreTwoText = 0;

    private void Start()
    {
        WinCanvas.SetActive(false);
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
                Victory("1");
            }
            else
            {
                other.gameObject.GetComponent<Controls>().ResetToStartPos();
                GameObject.FindGameObjectWithTag("PlayerTwo").GetComponent<Controls>().ResetToStartPos();
            }
            
        }
        else if (other.CompareTag("PlayerTwo"))
        {
            scoreTwoText++;
            playerTwoScoreText.text = $"Player Two: {scoreTwoText}";
            if(scoreTwoText >= 3)
            {
                Victory("2");
            }
            else
            {
                other.gameObject.GetComponent<Controls>().ResetToStartPos();
                GameObject.FindGameObjectWithTag("PlayerOne").GetComponent<Controls>().ResetToStartPos();
            }
            
        }

        
    }

    private void Victory(string victoriousPlayer)
    {
        Time.timeScale = 0;
        WinText.text = $"Player {victoriousPlayer} Won!";
        if(victoriousPlayer == "1")
        {
            WinText.color = GameObject.FindGameObjectWithTag("PlayerOne").GetComponent<PlayerDetails>().GetColor();
        }
        else if (victoriousPlayer == "2")
        {
            WinText.color = GameObject.FindGameObjectWithTag("PlayerTwo").GetComponent<PlayerDetails>().GetColor();
        }
        WinCanvas.SetActive(true);
    }
}

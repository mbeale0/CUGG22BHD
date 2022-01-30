using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private Text playerOneScoreText = null;
    [SerializeField] private Text playerTwoScoreText = null;
    [SerializeField] private GameObject WinCanvas = null;
    [SerializeField] private Text WinText = null;

    private int scoreOneText = 0;
    private int scoreTwoText = 0;
    private PlayerConfigData playerConfig;

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
        
        if (other.GetComponent<Controls>().GetPlayerConfig().PlayerIndex == 0)
        {
            scoreOneText++;
            playerOneScoreText.text = $"Player One: {scoreOneText}";
            if(scoreOneText >= 3)
            {
                Victory("1");
            }
            else
            {
                
            }
            
        }
        else if (other.GetComponent<Controls>().GetPlayerConfig().PlayerIndex == 1)
        {
            scoreTwoText++;
            playerTwoScoreText.text = $"Player Two: {scoreTwoText}";
            if(scoreTwoText >= 3)
            {
                Victory("2");
            }
            
        }
        Controls[] controls = FindObjectsOfType<Controls>();

        for (int i = 0; i < controls.Length; i++)
        {
            controls[i].ResetToStartPos();
        }


    }

    private void Victory(string victoriousPlayer)
    {
        Time.timeScale = 0;
        WinText.text = $"Player {victoriousPlayer} Won!";
        if(victoriousPlayer == "1")
        {
            WinText.color = PlayerConfigManager.Instance.GetColor(0);
        }
        else if (victoriousPlayer == "2")
        {
            WinText.color = PlayerConfigManager.Instance.GetColor(1);
        }
        WinCanvas.SetActive(true);
    }
    
}

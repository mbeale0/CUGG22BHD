using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSetupMenuController : MonoBehaviour
{
    [SerializeField] private Text PlayerTitleText = null;
    [SerializeField] private GameObject ReadyPanel = null;
    [SerializeField] private GameObject MenuPanel = null;
    [SerializeField] private Button ReadyButton = null;

    private int PlayerIndex;
    public bool inputEnabled = false;

    public void SetPlayerIndex(int pi)
    {
        PlayerIndex = pi;
        PlayerTitleText.text = $"Player {pi + 1}";     
    }
    private void Start()
    {
        ReadyPanel.SetActive(false);
    }

    public void SetPlayer(int character)
    {
        //PlayerConfigManager.Instance.SetPlayerColor(PlayerIndex, color);
        if(PlayerIndex == 0)
        {
            GameObject.FindGameObjectWithTag("PlayerSelection").GetComponent<PlayerSelection>().SetOneCharacter(character);
        }
        else if (PlayerIndex == 1)
        {
            GameObject.FindGameObjectWithTag("PlayerSelection").GetComponent<PlayerSelection>().SetTwoCharacter(character);
        }
        ReadyPanel.SetActive(true);
        ReadyButton.Select();
        MenuPanel.SetActive(false);
    }

    public void OnReadyButton()
    {
        PlayerConfigManager.Instance.ReadyPlayer(PlayerIndex);
        ReadyButton.gameObject.SetActive(false);
    }

   
}

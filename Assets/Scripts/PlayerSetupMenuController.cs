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

    public void SetColor(Material color)
    {
        PlayerConfigManager.Instance.SetPlayerColor(PlayerIndex, color);
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

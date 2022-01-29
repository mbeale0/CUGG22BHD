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
    private float ignoreInputTime = 1.5f;
    private bool inputEnabled = false;

    public void SetPlayerIndex(int pi)
    {
        PlayerIndex = pi;
        PlayerTitleText.text = $"Player {pi + 1}";
        ignoreInputTime = Time.time + ignoreInputTime;
    }
    private void Start()
    {
        ReadyPanel.SetActive(false);
        //ReadyButton.colors.normalColor = 255;
        //ReadyButton.colors.highlightedColor.a = 255;
    }
    void Update()
    {
        if(Time.time > ignoreInputTime)
        {
            inputEnabled = true;
        }
    }

    public void SetColor(Material color)
    {
        if (!inputEnabled)
        {
            return;
        }

        PlayerConfigManager.Instance.SetPlayerColor(PlayerIndex, color);
        ReadyPanel.SetActive(true);
        ReadyButton.Select();
        MenuPanel.SetActive(false);
    }

    public void OnReadyButton()
    {
        if(!inputEnabled)
        {
            return;
        }

        PlayerConfigManager.Instance.ReadyPlayer(PlayerIndex);

        ReadyButton.gameObject.SetActive(false);
    }

   
}

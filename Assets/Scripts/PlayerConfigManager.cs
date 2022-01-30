using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerConfigManager : MonoBehaviour
{
    [SerializeField] private PlayerInputManager playerInputManager = null;

    private List<PlayerConfigData> playerConfigs;
    private int MaxPlayers = 2;
    
    public static PlayerConfigManager Instance { get; private set; }

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.Log("SINGLETON - Trying to create another instance");
        }
        else
        {
            Instance = this;
            //DontDestroyOnLoad(Instance);
            playerConfigs = new List<PlayerConfigData>();
        }
    }

    public void SetPlayerColor(int index, Material color)
    {
        playerConfigs[index].PlayerMaterial = color;
    }
    public Color GetColor(int index)
    {
        return playerConfigs[index].PlayerMaterial.color;
    }
    public List<PlayerConfigData> GetPlayerConfigs()
    {
        return playerConfigs;
    }
    public void ReadyPlayer(int index)
    {
        playerConfigs[index].IsReady = true;
        if(playerConfigs.Count == MaxPlayers && playerConfigs.All(p => p.IsReady == true))
        {
            SceneManager.LoadScene(1);
           
        }
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.buildIndex == 1)
        {
            playerInputManager.splitScreen = !playerInputManager.splitScreen;
        }
        
    }
    public void HandlePlayerJoined(PlayerInput pi)
    {
        
        if(!playerConfigs.Any(p => p.PlayerIndex == pi.playerIndex))
        {
            pi.transform.SetParent(transform);
            playerConfigs.Add(new PlayerConfigData(pi));
        }
    }
}

public class PlayerConfigData
{
    public PlayerInput Input { get; set; }
    public int PlayerIndex { get; set; }
    public bool IsReady { get; set; }
    public Material PlayerMaterial { get; set; }
    public string PlayerTag { get; set; }
    public PlayerConfigData(PlayerInput pi)
    {
        PlayerIndex = pi.playerIndex;
        Input = pi;
    }
}

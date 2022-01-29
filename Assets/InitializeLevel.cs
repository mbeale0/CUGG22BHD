using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeLevel : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints = null;
    [SerializeField] private GameObject playerPrefab = null;


    private void Start()
    {        var playerConfigs = PlayerConfigManager.Instance.GetPlayerConfigs().ToArray();        for(int i = 0; i < playerConfigs.Length; i++){
            var player = Instantiate(playerPrefab, spawnPoints[i].position, spawnPoints[i].rotation, gameObject.transform);
            player.GetComponent<Controls>().InitializePlayer(playerConfigs[i]);
        }    }

}

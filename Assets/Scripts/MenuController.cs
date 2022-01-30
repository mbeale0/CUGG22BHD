using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    void Update()
    {

    }
    public void OnMainMenu()
    {
        Destroy(PlayerConfigManager.Instance);
        SceneManager.LoadScene(0);
    }
}

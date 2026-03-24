using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    GameObject creditsScreen;

    public void HandleCreditsScreen(bool enable)
    {
        creditsScreen.SetActive(enable);
    }

    public void StartGame()
    {
        if (creditsScreen.activeSelf) return;

        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }
}

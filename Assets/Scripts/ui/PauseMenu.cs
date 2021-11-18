using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject settingsMenu;

    private void Start()
    {
        Time.timeScale = 0;
    }


    public void OpenSettingsMenu()
    {
        uiManager.instance.OpenMenu(settingsMenu);
        gameObject.SetActive(false);
    }
    public void ReturnToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
    public void ReturnToGame()
    {
        Time.timeScale = 1;
        uiManager.instance.CloseMenu(gameObject);
    }
}

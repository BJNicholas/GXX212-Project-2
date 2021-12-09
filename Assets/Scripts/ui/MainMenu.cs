using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject main, credits;
    public void OpenCreditsPanel()
    {
        credits.SetActive(true);
        main.SetActive(false);
    }
    public void ReturnToMain()
    {
        credits.SetActive(false);
        main.SetActive(true);
    }
}

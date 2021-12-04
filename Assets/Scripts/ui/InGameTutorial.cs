using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameTutorial : MonoBehaviour
{
    public GameObject narrativeUI;
    public Text narrativeText;
    public GameObject confirmButton;

    public AudioSource inGameVO1;
    public AudioSource inGameVO2;
    public AudioSource inGameVO3;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartInGameIntro());
    }

    IEnumerator StartInGameIntro()
    {
        yield return new WaitForSeconds(6f);
        narrativeUI.gameObject.SetActive(true);
        StartCoroutine(StartInGameHelp());
    }

    IEnumerator StartInGameHelp()
    {
        confirmButton.SetActive(false);
        narrativeText.text =
            ("Welcome to Earth! We wish you the best of luck on your mission to kill all the infected. Be sure to stay alive and pay attention to the clock.");
        inGameVO1.Play();
        yield return new WaitForSeconds(11f);
        StartCoroutine(EndInGameHelp());
    }

    IEnumerator EndInGameHelp()
    {
        narrativeText.text =
            ("Your generated dome only works in the morning so make sure to arm yourself with robots and barricade yourself before nightfall.");
        inGameVO2.Play();
        yield return new WaitForSeconds(10f);
        UnLockMouse();
        confirmButton.SetActive(true);
        narrativeText.text =
            ("When all goes well, we'll send someone to pick you up on Day 10. Goodluck out there, B.I.N. Agent.");
        inGameVO3.Play();
    }

    public void ConfirmButton()
    {
        narrativeUI.SetActive(false);
        Camera.main.GetComponent<MouseLook>().enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void UnLockMouse()
    {
        Camera.main.GetComponent<MouseLook>().enabled = false;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

}

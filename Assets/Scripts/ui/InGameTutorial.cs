using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameTutorial : MonoBehaviour
{
    public GameObject narrativeUI;
    public Text narrativeText;

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
        UnLockMouse();
        StartCoroutine(StartInGameHelp());
    }

    IEnumerator StartInGameHelp()
    {
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
        narrativeText.text =
            ("When all goes well, we'll send someone to pick you up on Day 10. Goodluck out there, B.I.N. Agent.");
        inGameVO3.Play();
    }

    public void ConfirmButton()
    {
        narrativeUI.SetActive(false);
        StopAllCoroutines();
        inGameVO1.Stop();
        inGameVO2.Stop();
        inGameVO3.Stop();
        LockMouse();
    }

    private void UnLockMouse()
    {
        Camera.main.GetComponent<MouseLook>().enabled = false;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    private void LockMouse()
    {
        Camera.main.GetComponent<MouseLook>().enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

}

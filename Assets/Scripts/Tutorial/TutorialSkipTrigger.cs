using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialSkipTrigger : MonoBehaviour
{
    public GameObject tutorialSkipUI;

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Friendly")
        {
            tutorialSkipUI.SetActive(true);
            Camera.main.GetComponent<MouseLook>().enabled = false;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
    }

    public void Proceed()
    {
        tutorialSkipUI.SetActive(false);
        Camera.main.GetComponent<MouseLook>().enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        SceneManager.LoadScene(sceneName: "Main");
    }

    public void Return()
    {
        tutorialSkipUI.SetActive(false);
        Camera.main.GetComponent<MouseLook>().enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

}

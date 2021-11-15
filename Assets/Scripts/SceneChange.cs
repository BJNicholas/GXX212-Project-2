using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    //UI for confirmation message
    public GameObject confirmationPanel;

    //PlayButton is pressed
    public void PlayGame()
    {
        //Debug.Log("Playing Game...");
        confirmationPanel.SetActive(true);
        //SceneManager.LoadScene(sceneName: "Main");
    }

    //Yes button is pressed
    public void YesConfirm()
    {
        SceneManager.LoadScene(sceneName: "Test");
    }

    //No button is pressed
    public void NoConfirm()
    {
        confirmationPanel.SetActive(false);
    }


    public void Quit()
    {
        Debug.Log("Quitting Scene...");
        Application.Quit();
    }

    //for planned Scene Transition Animations
    /*
    IEnumerator LoadScene()
    {
        transitionAnim.SetTrigger("end");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(sceneName);
    }
    */

}

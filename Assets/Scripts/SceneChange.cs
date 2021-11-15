using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    //PlayButton is pressed
    public void PlayGame()
    {
        Debug.Log("Playing Game...");
        SceneManager.LoadScene(sceneBuildIndex: 1);
    }

    /*public void StartTutorial()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Debug.Log("Start Tutorial...");
    }
    
    public void SkipTutorial()
    {
        SceneManager.LoadScene(sceneName: "GameScene");
        Debug.Log("Skip Tutorial...");
    }
    */

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

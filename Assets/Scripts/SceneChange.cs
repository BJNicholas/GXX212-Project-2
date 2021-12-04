using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{

    public GameObject mainMenuUI;
    public GameObject narrativeUI;
    public Text narrativeText;

    public AudioSource mainMenuVO1;
    public AudioSource mainMenuVO2;
    public AudioSource mainMenuVO3;
    public AudioSource mainMenuVO4;

    public void PlayGame()
    {
        //StartCoroutine(LoadNarrative());
        mainMenuUI.SetActive(false);
        narrativeUI.SetActive(true);
        StartCoroutine(StartNarrative1());
    }

    public void Quit()
    {
        Debug.Log("Quitting Scene...");
        Application.Quit();
    }

    public void SkipNarrative()
    {
        StopAllCoroutines();
        print("Change to Tutorial Scene...");
        SceneManager.LoadScene(2);
    }

    //IEnumerator LoadNarrative()
    //{
    //    print("Fading Animation");
    //    yield return new WaitForSeconds(2f);
    //    mainMenuUI.SetActive(false);
    //    narrativeUI.SetActive(true);
    //    StartCoroutine(StartNarrative1());
    //}

    IEnumerator StartNarrative1()
    {
        narrativeText.text =
            ("The human race has left the Earth poisoned by the ignorance of man’s technological advancements. " +
            "For centuries, humans have lived in space to survive until life on earth starts to prosper again.");
        mainMenuVO1.Play();
        yield return new WaitForSeconds(14f);
        mainMenuVO1.Stop();
        StartCoroutine(StartNarrative2());
    }

    IEnumerator StartNarrative2()
    {
        narrativeText.text =
            ("Finally, after centuries of waiting, the Earth started to show signs of life. Grass, trees, oceans, and everything beautiful have been coming back after all this time.");
        mainMenuVO2.Play();
        yield return new WaitForSeconds(14f);
        mainMenuVO2.Stop();
        StartCoroutine(StartNarrative3());
    }

    IEnumerator StartNarrative3()
    {
        narrativeText.text =
            ("Unfortunately everyone left behind on earth have been infected by the toxic gases. Now they crave the flesh and blood of the humans " +
            "who left them to die all those years ago.");
        mainMenuVO3.Play();
        yield return new WaitForSeconds(12f);
        mainMenuVO3.Stop();
        StartCoroutine(StartNarrative4());
    }

    IEnumerator StartNarrative4()
    {
        narrativeText.text =
            ("It now falls under the job of the Bio-Infected Neutraliser (BIN) Agent to take out all the infected and finally bring all humans back home.");
        mainMenuVO4.Play();
        yield return new WaitForSeconds(12f);
        print("Changing Scene...");
        SceneManager.LoadScene(2);
    }
}

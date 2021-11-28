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
        yield return new WaitForSeconds(12f);
        StartCoroutine(StartNarrative2());
    }

    IEnumerator StartNarrative2()
    {
        narrativeText.text =
            ("Finally, after centuries of waiting, the Earth started to show signs of life. Grass, trees, oceans, and everything beautiful have been coming back after all this time.");
        yield return new WaitForSeconds(10f);
        StartCoroutine(StartNarrative3());
    }

    IEnumerator StartNarrative3()
    {
        narrativeText.text =
            ("Although it seemed too good to be true, everyone left behind on earth have been infected by the toxic gases and now craves the flesh and blood of the humans " +
            "who left them to die all those years ago.");
        yield return new WaitForSeconds(12f);
        StartCoroutine(StartNarrative4());
    }

    IEnumerator StartNarrative4()
    {
        narrativeText.text =
            ("It now falls under the job of the B.I.N. Agent to take out all the infected and finally bring all humans back home.");
        yield return new WaitForSeconds(10f);
        print("Changing Scene...");
        //SceneManager.LoadScene(sceneName: "Tutorial");
    }
}

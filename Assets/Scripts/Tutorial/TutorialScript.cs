using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialScript : MonoBehaviour
{

    public GameObject tutorialIntro;
    public Text logText;
    
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        StartCoroutine(PlayTutorialIntro());
    }

    public void ShootTutorial()
    {
        StartCoroutine(PlayShootTutorial());
    }

    public void RobotTutorial()
    {
        StartCoroutine(PlayRobotTutorial());
    }

    public void BuildingTutorial()
    {
        StartCoroutine(PlayBuildingTutorial());
    }

    IEnumerator PlayTutorialIntro()
    {
        logText.text = ("Welcome to the B.I.N training simulation room.");
        tutorialIntro.SetActive(true);
        yield return new WaitForSeconds(5f);
        StartCoroutine(MovementTutorialIntro());
    }

    IEnumerator MovementTutorialIntro()
    {
        logText.text = ("WASD - Move \n Hold SHIFT - Run \n SPACE - Jump");
        yield return new WaitForSeconds(8f);
        tutorialIntro.SetActive(false);
    }

    IEnumerator PlayShootTutorial()
    {
        logText.text = ("Left Click - Shoot \n Right Click - Aim Down Sights \n R - Reload");
        tutorialIntro.SetActive(true);
        yield return new WaitForSeconds(8f);
        tutorialIntro.SetActive(false);
    }

    IEnumerator PlayRobotTutorial()
    {
        logText.text = ("Robots will be created throughout the day \n TAB - Open Robot Orders \n NOTE: FACTORY is required to spawn Robots.");
        tutorialIntro.SetActive(true);
        yield return new WaitForSeconds(8f);
        tutorialIntro.SetActive(false);
    }

    IEnumerator PlayBuildingTutorial()
    {
        logText.text = ("F - Pick Up Items \n E - Open Inventory/Crafting Menu \n Scroll Wheel - Change Equipped/Rotate Building");
        tutorialIntro.SetActive(true);
        yield return new WaitForSeconds(8f);
        tutorialIntro.SetActive(false);
    }

}

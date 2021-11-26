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
        yield return new WaitForSeconds(8f);
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
        logText.text = ("P - Spawn Robot \n TAB - Open Robot Orders \n NOTE: Need to build FACTORY to spawn Robots.");
        tutorialIntro.SetActive(true);
        yield return new WaitForSeconds(8f);
        tutorialIntro.SetActive(false);
    }

    IEnumerator PlayBuildingTutorial()
    {
        logText.text = ("F - Pick Up Items \n E - Open Inventory/Crafting Menu");
        tutorialIntro.SetActive(true);
        yield return new WaitForSeconds(8f);
        tutorialIntro.SetActive(false);
    }

}

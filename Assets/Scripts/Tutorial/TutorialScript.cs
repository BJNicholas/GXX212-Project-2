using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;

public class TutorialScript : MonoBehaviour
{

    public GameObject tutorialIntro;
    public Text logText;
    public PlayableDirector tutorialCutscene;
    
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;        
        StartCoroutine(PlayTutorialIntro());
    }

    public void MovementTutorial()
    {
        StartCoroutine(PlayMovementTutorial());
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
        tutorialCutscene.Play();
        logText.text = ("Welcome to the B.I.N training simulation.");
        tutorialIntro.SetActive(true);
        yield return new WaitForSeconds(3f);
        StartCoroutine(PlayTutorial1());
    }

    IEnumerator PlayTutorial1()
    {
        logText.text = ("You will learn all you need to know for your mission on Earth such as: ");
        yield return new WaitForSeconds(3.5f);
        StartCoroutine(PlayTutorial2());
    }

    IEnumerator PlayTutorial2()
    {
        logText.text = ("1. Movement\n 2. How to Shoot your Weapon\n 3. Controlling your Robot Minions\n 4. Building and Crafting Tools or Structures.");
        yield return new WaitForSeconds(9.5f);
        StartCoroutine(PlayTutorial3());
    }

    IEnumerator PlayTutorial3()
    {
        logText.text = ("If you wish to skip this simulation, \n proceed to the exit tube to commence your mission.");
        yield return new WaitForSeconds(5f);
        StartCoroutine(PlayTutorialEnd());
    }

    IEnumerator PlayTutorialEnd()
    {
        logText.text = ("Goodluck, B.I.N. Agent.");
        yield return new WaitForSeconds(5f);
        tutorialCutscene.Pause();
        tutorialIntro.SetActive(false);
    }

    IEnumerator PlayMovementTutorial()
    {
        logText.text = ("WASD - Move \n Hold SHIFT - Run \n SPACE - Jump");
        tutorialIntro.SetActive(true);
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
        logText.text = ("With a Factory constructed: Robots will be created throughout the day \n TAB - Open Robot Orders \n With Scanner equipped: Left Click - Enter Robot Perspective" +
            "\n With Scanner equipped: Right Click - Exit Robot Perspective");
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

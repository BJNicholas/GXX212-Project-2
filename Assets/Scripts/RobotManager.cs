using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotManager : MonoBehaviour
{
    public static RobotManager instance;
    public List<GameObject> allRobots;
    public GameObject currentRobot;

    public Camera playerCam;

    private void Start()
    {
        playerCam = Camera.main;
        instance = this;
        currentRobot = null;
    }

    private void Update()
    {
        allRobots.Clear();
        //filling allRobots list
        foreach (GameObject character in GameObject.FindGameObjectsWithTag("Friendly"))
        {
            if (character.GetComponent<Robot>() == true)
            {
                if (allRobots.Contains(character)) print("All ready in list"); // please remove this later
                else
                {
                    allRobots.Add(character);
                }
            }
        }

        if(currentRobot != null)
        {
            playerCam.depth = 0;
            uiManager.instance.gameObject.SetActive(false);
            currentRobot.GetComponent<Robot>().robotCam.depth = 1;
        }
        else
        {
            playerCam.depth = 1;
            uiManager.instance.gameObject.SetActive(true);
        }
    }

    public void NextRobot()
    {
        if(currentRobot == null)
        {
            currentRobot = allRobots[0];
            currentRobot.GetComponent<Robot>().robotCam.depth = 1;
        }
        else
        {
            GameObject oldRobot = currentRobot;
            oldRobot.GetComponent<Robot>().robotCam.depth = 0;
            if(allRobots.IndexOf(currentRobot) == allRobots.ToArray().Length - 1)
            {
                currentRobot = allRobots[0];
            }
            else currentRobot = allRobots[allRobots.IndexOf(currentRobot) + 1];
            currentRobot.GetComponent<Robot>().robotCam.depth = 1;
        }
    }
    public void BackToPlayer()
    {
        currentRobot.GetComponent<Robot>().robotCam.depth = 0;
        currentRobot = null;
        playerCam.depth = 1;
    }
}

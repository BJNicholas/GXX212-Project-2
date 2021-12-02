using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrdersMenu : MonoBehaviour
{
    public static OrdersMenu instance;
    public List<GameObject> allRobots;
    public Text robotCount;

    private void Start()
    {
        instance = this;
    }

    private void Update()
    {
        allRobots.Clear();
        //filling allRobots list
        foreach(GameObject character in GameObject.FindGameObjectsWithTag("Friendly"))
        {
            if(character.GetComponent<Robot>() == true)
            {
                if (allRobots.Contains(character)) print("All ready in list"); // please remove this later
                else
                {
                    allRobots.Add(character);
                }
            }
        }
        robotCount.text = (allRobots.ToArray().Length).ToString();

        if (Input.GetKeyDown(KeyCode.Alpha1)) Follow();
        else if (Input.GetKeyDown(KeyCode.Alpha2)) Attack();
        else if (Input.GetKeyDown(KeyCode.Alpha3)) Defend();
        else if (Input.GetKeyDown(KeyCode.Alpha4)) Collect();
        else if (Input.GetKeyDown(KeyCode.Alpha5)) CancelOrderChange();
    }


    void Follow()
    {
        foreach(GameObject robot in allRobots)
        {
            robot.GetComponent<Robot>().currentState = MinionManager.states.following;
        }
        gameObject.SetActive(false);
    }
    void Attack()
    {
        foreach (GameObject robot in allRobots)
        {
            robot.GetComponent<Robot>().currentState = MinionManager.states.attacking;
        }
        gameObject.SetActive(false);
    }
    void Defend()
    {
        foreach (GameObject robot in allRobots)
        {
            robot.GetComponent<Robot>().currentState = MinionManager.states.defending;
        }
        gameObject.SetActive(false);
    }
    void Collect()
    {
        foreach (GameObject robot in allRobots)
        {
            robot.GetComponent<Robot>().currentState = MinionManager.states.collectingResources;
        }
        gameObject.SetActive(false);
    }
    void CancelOrderChange()
    {
        gameObject.SetActive(false);
    }
}

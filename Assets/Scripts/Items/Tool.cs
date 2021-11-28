using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool : MonoBehaviour
{
    public string toolName;

    private void Start()
    {
        GetComponent<BoxCollider>().enabled = false;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GetComponent<Animator>().Play(toolName + "-Swing");
            if(toolName == "Remote")
            {
                RobotManager.instance.NextRobot();
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            if (toolName == "Remote")
            {
                RobotManager.instance.BackToPlayer();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Zombie")
        {
            other.gameObject.GetComponent<Character>().health -= 25;
        }
    }
}

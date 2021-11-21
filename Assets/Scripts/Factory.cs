using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour
{
    public GameObject robotPrefab;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) SpawnRobot();
    }


    public void SpawnRobot()
    {
        GameObject robot = Instantiate(robotPrefab, transform);
        robot.transform.position = gameObject.transform.position;
    }
}

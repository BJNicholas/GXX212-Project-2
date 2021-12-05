using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour
{
    public GameObject robotPrefab;
    public float coolDown = 100;

    float count;
    private void Start()
    {
        count = coolDown;
    }
    private void Update()
    {
        count--;
        if(count <= 0)
        {
            SpawnRobot();
            count = coolDown;
        }
    }


    public void SpawnRobot()
    {
        GameObject robot = Instantiate(robotPrefab, transform);
        robot.transform.position = gameObject.transform.position;
    }
}

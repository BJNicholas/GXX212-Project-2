using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject zombiePrefab;
    bool nightTime = false;
    public float coolDown = 50;
    public int maxPerDay = 20;
    float count = 0;
    GameObject manager;

    private void Start()
    {
        manager = GameObject.Find("GameManager");
    }

    private void Update()
    { 
        count += 0.1f;
        if((count >= coolDown && nightTime) && maxPerDay > 0)
        {
            SpawnZombie();
            maxPerDay -= 1;
            count = 0;
        }


        if (manager.GetComponent<LightingManager>().timeOfDay > 18 || manager.GetComponent<LightingManager>().timeOfDay < 6) nightTime = true;
        else nightTime = false;
    }




    public void SpawnZombie()
    {
        GameObject zombie = Instantiate(zombiePrefab, transform);
    }
}

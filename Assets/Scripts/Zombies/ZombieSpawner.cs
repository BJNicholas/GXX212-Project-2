using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject zombiePrefab;
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
        if(count >= coolDown && maxPerDay > 0)
        {
            SpawnZombie();
            maxPerDay -= 1;
            count = 0;
        }
    }




    public void SpawnZombie()
    {
        GameObject zombie = Instantiate(zombiePrefab, transform);
    }
}

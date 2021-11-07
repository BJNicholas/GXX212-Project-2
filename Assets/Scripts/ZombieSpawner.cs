using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject zombiePrefab;
    public bool nightTime = false;
    public float coolDown = 50;
    float count = 0;
    private void Update()
    {
        count += 0.1f;
        if(count >= coolDown && nightTime)
        {
            SpawnZombie();
            count = 0;
        }
    }




    public void SpawnZombie()
    {
        GameObject zombie = Instantiate(zombiePrefab);
        zombie.transform.position = gameObject.transform.position;
    }
}

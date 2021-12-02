using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Structure : MonoBehaviour
{
    public float health = 100;


    private void Update()
    {
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
}

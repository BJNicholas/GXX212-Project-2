using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieScript : MonoBehaviour
{
    public float health;


    private void FixedUpdate()
    {

        if(health <= 0)
        {
            Die();
        }
        //While alive
        else
        {
            GetComponent<NavMeshAgent>().SetDestination(PlayerMovement.instance.gameObject.transform.position); // this will change!!!!!
        }

    }

    public void Die()
    {
        GetComponent<NavMeshAgent>().enabled = false;
        //GetComponentInChildren<Rigidbody>().detectCollisions = false;
        GetComponent<ZombieScript>().enabled = false;
        Destroy(gameObject);
    }
}

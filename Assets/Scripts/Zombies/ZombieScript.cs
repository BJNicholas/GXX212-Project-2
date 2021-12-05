using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieScript : MonoBehaviour
{
    public float damage;


    private void FixedUpdate()
    {

        Attack();

    }

    void Attack()
    {
        GameObject targetCharacter = null;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Friendly");
        enemies = GameObject.FindGameObjectsWithTag("Structure");
        float closestDistance = Mathf.Infinity;
        foreach (GameObject character in enemies)
        {
            if (Vector3.Distance(gameObject.transform.position, character.transform.position) <= closestDistance)
            {
                targetCharacter = character;
                closestDistance = Vector3.Distance(gameObject.transform.position, character.transform.position);
            }
        }

        if (targetCharacter != null) GetComponent<NavMeshAgent>().SetDestination(targetCharacter.transform.position);
        else print("Looking for brains");
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Friendly" || other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Character>().health -= damage;
        }
        if (other.gameObject.GetComponent<Structure>())
        {
            other.gameObject.GetComponent<Structure>().health -= damage;
        }

        if (other.gameObject.transform.parent.GetComponent<Structure>())
        {
            print("Ill blow you house down");
            other.gameObject.transform.parent.GetComponent<Structure>().health -= damage;
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.GetComponent<Structure>())
        {
            print("Ill blow you house down");
            collision.gameObject.transform.parent.GetComponent<Structure>().health -= damage;
        }
    }
}

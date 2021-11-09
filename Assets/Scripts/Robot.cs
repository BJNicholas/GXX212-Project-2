using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Robot : MonoBehaviour
{
    public MinionManager.states currentState;
    NavMeshAgent navAgent;
    public GameObject hand;
    bool reloading = false;
    [Header("Stats")]
    public float health = 100;

    private void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        hand.transform.GetChild(0).gameObject.GetComponent<AudioSource>().volume = 0.1f;
    }

    private void Update()
    {
        RaycastHit hit;
        Vector3 point;
        Debug.DrawRay(hand.transform.position, hand.transform.forward, Color.red, 1);
        if (Physics.Raycast(hand.transform.position, hand.transform.forward, out hit, 100))
        {
            //the point exactly where the cast hit
            point = hit.point;

            //if hit zombie
            if (hit.transform.gameObject.tag == "Zombie")
            {
                hand.transform.GetChild(0).gameObject.GetComponent<GunScript>().shooting = true;
            }
            else
            {
                hand.transform.GetChild(0).gameObject.GetComponent<GunScript>().shooting = false;
            }
        }
        if (currentState == MinionManager.states.idle)
        {
            Idle();
        }
        else if (currentState == MinionManager.states.movingToPoint)
        {
            MoveToPoint(new Vector3(3,0,5));
        }
        else if (currentState == MinionManager.states.attacking)
        {
            Attack();
        }
    }

    void Idle()
    {
        print("waiting for orders");
    }
    void MoveToPoint(Vector3 target)
    {
        navAgent.SetDestination(target);
        if(navAgent.transform.position == target)
        {
            currentState = MinionManager.states.idle;
        }
    }
    void Attack()
    {
        GameObject[] allZombies = GameObject.FindGameObjectsWithTag("Zombie");
        GameObject targetZombie = null;
        float closestDistance = 5000;
        foreach(GameObject zombie in allZombies)
        {
            if(Vector3.Distance(gameObject.transform.position, zombie.transform.position) <= closestDistance)
            {
                targetZombie = zombie;
                closestDistance = Vector3.Distance(gameObject.transform.position, zombie.transform.position);
            }
        }
        if (targetZombie != null && Vector3.Distance(gameObject.transform.position, targetZombie.transform.position) <= 10)
        {
            navAgent.SetDestination(targetZombie.transform.position);
        }

        else
        {
            print("Looking for zombies");
        }
        if(hand.transform.GetChild(0).gameObject.GetComponent<GunScript>().magAmmo == 0 && reloading == false)
        {
            reloading = true;
            currentState = MinionManager.states.reloading;
            MoveToPoint(-targetZombie.transform.forward * 10);
            Invoke("Reload", 2.5f);
        }

    }

    void Reload()
    {
        hand.transform.GetChild(0).gameObject.GetComponent<GunScript>().Reload();
        reloading = false;
        currentState = MinionManager.states.attacking;
    }


}

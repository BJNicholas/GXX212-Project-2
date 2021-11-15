using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Robot : MonoBehaviour
{
    public MinionManager.states currentState;
    NavMeshAgent navAgent;
    public GameObject gun;
    [HideInInspector]GameObject targetZombie;
    bool reloading = false;

    private void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
    }

    private void FixedUpdate()
    {
        RaycastHit hit;
        Vector3 point;
        Debug.DrawRay(gun.transform.position, gun.transform.forward, Color.red, 1);
        if (Physics.Raycast(gun.transform.position, gun.transform.forward, out hit, 500))
        {
            //the point exactly where the cast hit
            point = hit.point;

            //if hit zombie
            if (hit.transform.gameObject.tag == "Zombie")
            {
                gun.GetComponent<RobotGun>().shooting = true;
            }
            else
            {
                gun.GetComponent<RobotGun>().shooting = false;
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
        else if (currentState == MinionManager.states.defending)
        {
            Defend();
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
        targetZombie = null;
        GameObject[] allZombies = GameObject.FindGameObjectsWithTag("Zombie");
        float closestDistance = Mathf.Infinity;
        foreach(GameObject zombie in allZombies)
        {
            if(Vector3.Distance(gameObject.transform.position, zombie.transform.position) <= closestDistance)
            {
                targetZombie = zombie;
                closestDistance = Vector3.Distance(gameObject.transform.position, zombie.transform.position);
            }
        }
        if (targetZombie != null)
        {
            navAgent.SetDestination((targetZombie.transform.position) - ((targetZombie.transform.position - gameObject.transform.position) * 1.5f));
            gameObject.transform.GetChild(0).LookAt(targetZombie.transform);
        }
        else
        {
            print("Looking for zombies");
        }
        if(gun.GetComponent<RobotGun>().magAmmo == 0 && reloading == false)
        {
            reloading = true;
            MoveToPoint(-targetZombie.transform.forward * 10);
            Invoke("Reload", 2.5f);
        }

    }
    void Defend()
    {
        GameObject[] allZombies = GameObject.FindGameObjectsWithTag("Zombie");
        float closestDistance = 5000;
        foreach (GameObject zombie in allZombies)
        {
            if (Vector3.Distance(gameObject.transform.position, zombie.transform.position) <= closestDistance)
            {
                targetZombie = zombie;
                closestDistance = Vector3.Distance(gameObject.transform.position, zombie.transform.position);
            }
        }
        if (targetZombie != null && Vector3.Distance(gameObject.transform.position, targetZombie.transform.position) <= 20)
        {
            gameObject.transform.GetChild(0).LookAt(targetZombie.transform);
        }


        if (gun.GetComponent<RobotGun>().magAmmo == 0 && reloading == false)
        {
            reloading = true;
            Invoke("Reload", 2.5f);
        }
    }

    void Reload()
    {
        gun.GetComponent<RobotGun>().Reload();
        reloading = false;
    }


}

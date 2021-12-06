using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Robot : MonoBehaviour
{
    public MinionManager.states currentState;
    GameObject player;
    NavMeshAgent navAgent;
    public Camera robotCam;
    public GameObject gun;
    [HideInInspector]GameObject targetZombie;
    [HideInInspector]GameObject targetResource;

    bool reloading = false;

    private void Start()
    {
        robotCam.depth = 0;
        robotCam.enabled = false;
        navAgent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player Body");
        CollectResources();
    }

    private void FixedUpdate()
    {
        //raycasy to see if zombie is infront of robot
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

        //statemachine
        if (currentState == MinionManager.states.idle)
        {
            Idle();
        }
        else if (currentState == MinionManager.states.movingToPoint)
        {
            MoveToPoint(new Vector3(3, 0, 5));
        }
        else if (currentState == MinionManager.states.attacking)
        {
            Attack();
        }
        else if (currentState == MinionManager.states.defending)
        {
            Defend();
        }
        else if (currentState == MinionManager.states.following)
        {
            FollowPlayer();
        }
        else if (currentState == MinionManager.states.collectingResources)
        {
            CollectResources();
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
            //direction and distance 
            //newPos = distance - how far away I want them to stand
            //setDestination(newPos)
            Vector3 heading = targetZombie.transform.position - gameObject.transform.position;
            float distance = heading.magnitude;
            Vector3 direction = heading / distance;
            if (heading.sqrMagnitude < 5 * 5)
            {
                navAgent.Stop();
            } 
            else
            {
                navAgent.Resume();
                navAgent.SetDestination(targetZombie.transform.position);
            }
            gameObject.transform.LookAt(targetZombie.transform);
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
    void FollowPlayer()
    {
        Vector3 heading = player.transform.position - gameObject.transform.position;
        float distance = heading.magnitude;
        Vector3 direction = heading / distance;
        if (heading.sqrMagnitude < 5 * 5)
        {
            navAgent.Stop();
            print("Close to Player");
        }
        else
        {
            navAgent.Resume();
            navAgent.SetDestination(player.transform.position);
            print("Moving to Player");
        }
        //look at nearby zombies
        targetZombie = null;
        GameObject[] allZombies = GameObject.FindGameObjectsWithTag("Zombie");
        float closestDistance = 20f;
        foreach (GameObject zombie in allZombies)
        {
            if (Vector3.Distance(gameObject.transform.position, zombie.transform.position) <= closestDistance)
            {
                targetZombie = zombie;
                closestDistance = Vector3.Distance(gameObject.transform.position, zombie.transform.position);
            }
        }
        if (targetZombie != null) gameObject.transform.LookAt(targetZombie.transform);
        else gameObject.transform.LookAt(player.transform);

        if (gun.GetComponent<RobotGun>().magAmmo == 0 && reloading == false)
        {
            reloading = true;
            MoveToPoint(-targetZombie.transform.forward * 10);
            Invoke("Reload", 2.5f);
        }
    }
    void CollectResources()
    {
        GameObject[] resources = GameObject.FindGameObjectsWithTag("Resource");
        float closestDistance = Mathf.Infinity;
        foreach (GameObject resource in resources)
        {
            if(resource.GetComponent<Resource>().health > 0)
            {
                if (Vector3.Distance(gameObject.transform.position, resource.transform.position) <= closestDistance)
                {
                    targetResource = resource;
                    closestDistance = Vector3.Distance(gameObject.transform.position, resource.transform.position);
                }
            }
        }
        if (targetResource == null)
        {
            print("Looking for Resources");
            navAgent.SetDestination(transform.position + new Vector3(Random.Range(1, 10), 0, Random.Range(1, 10)));
            PlaceObjectsOnTerrain.instance.SpawnTreeObjects(gameObject);
        }

        if(targetResource != null)
        {
            navAgent.SetDestination(targetResource.transform.position);
            gameObject.transform.LookAt(targetResource.transform);
            if(Vector3.Distance(targetResource.transform.position, gameObject.transform.position) < 3)
            {
                TakeResource(50);
            }
        }
    }
    float count = 0;
    void TakeResource(float delay)
    {
        count -= 1;
        if(count <= 0)
        {
            targetResource.GetComponent<AudioSource>().clip = targetResource.GetComponent<Resource>().collectionSound;
            targetResource.GetComponent<AudioSource>().Play();
            targetResource.GetComponent<Resource>().health -= 5;
            InventoryManager.instance.StoreItem(targetResource.GetComponent<Resource>().collectableItem, targetResource.GetComponent<Resource>().amount);
            if (targetResource.GetComponent<Resource>().health <= 0)
            {
                targetResource.GetComponent<Resource>().Death();
            }
            count = delay;
        }
    }

    void Reload()
    {
        gun.GetComponent<RobotGun>().Reload();
        reloading = false;
    }


}

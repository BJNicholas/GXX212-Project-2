using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    public GameObject barrel;
    public float currentAmmo, magAmmo, maxMagAmmo, range, damage; //add fire rate + auto

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        { 
            RaycastHit hit;
            Vector3 point;
            Debug.DrawRay(barrel.transform.position, barrel.transform.forward, Color.red, range);
            if (Physics.Raycast(barrel.transform.position, barrel.transform.forward, out hit, range))
            {
                //the point exactly where the bullet hit
                point = hit.point;
                //if hit zombie
                if(hit.transform.gameObject.tag == "Zombie")
                {
                    print("HIT");
                    hit.transform.gameObject.GetComponentInParent<ZombieScript>().health -= damage;
                }
            }
        }
    }

}

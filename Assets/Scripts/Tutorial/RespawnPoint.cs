using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPoint : MonoBehaviour
{
    public Transform respawnPoint;

    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Friendly")
        {
            print("Respawn");
            transform.position = respawnPoint.position;
        }
    }

}

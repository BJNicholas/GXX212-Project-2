using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPoint : MonoBehaviour
{
    public Transform respawnPoint;
    public Transform Player;

    private void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag("Friendly"))
        {
            print("Respawn");
            Player.transform.position = respawnPoint.position;
            Physics.SyncTransforms();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticDoor : MonoBehaviour
{

    public GameObject movingDoor;

    public float maxOpening = -7f;
    public float maxClosing = 0f;

    public float doorSpeed = 15f;

    bool playerIsHere;

    // Update is called once per frame
    void Update()
    {
        if(playerIsHere)
        {
            if(movingDoor.transform.position.y > maxOpening)
            {
                movingDoor.transform.Translate(0f, -doorSpeed * Time.deltaTime, 0f);
            }
        }
        else
        {
            if (movingDoor.transform.position.y < maxClosing)
            {
                movingDoor.transform.Translate(0f, doorSpeed * Time.deltaTime, 0f);
            }
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Friendly")
        {
            playerIsHere = true;
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Friendly")
        {
            playerIsHere = false;
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    public GameObject collectableItem;
    public GameObject requiredTool;
    public AudioClip collectionSound;
    public int amount;
    public float health = 100;

    private void OnMouseOver()
    {
        if (Input.GetKeyDown(KeyCode.F) && requiredTool == null)
        {
            health -= 100;
            print("Collect " + collectableItem.ToString() + " X" + amount);
            InventoryManager.instance.StoreItem(collectableItem, amount);
            Death();
        }
    }
    public void Death()
    {
        Destroy(gameObject);
        //add more
    }
    private void OnTriggerEnter(Collider other)
    {
        if(requiredTool != null)
        {
            if (other.gameObject.GetComponent<Tool>().toolName == requiredTool.GetComponent<Tool>().toolName)
            {
                GetComponent<AudioSource>().clip = collectionSound;
                GetComponent<AudioSource>().Play();
                health -= 5;
                InventoryManager.instance.StoreItem(collectableItem, amount);
                if(health <= 0)
                {
                    Death();
                }
            }
        }
        if (other.gameObject.GetComponent<Robot>())
        {
            GetComponent<AudioSource>().clip = collectionSound;
            GetComponent<AudioSource>().Play();
            health -= 5;
            InventoryManager.instance.StoreItem(collectableItem, amount);
            if (health <= 0)
            {
                Death();
            }
        }
    }

}

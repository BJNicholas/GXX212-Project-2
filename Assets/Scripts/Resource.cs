using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    public GameObject collectableItem;
    public int amount;
    public float health = 100;


    private void OnMouseOver()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            health -= 10; 
            print("Collect " + collectableItem.ToString() + " X" + amount);
            InventoryManager.instance.AddItem(collectableItem, amount);

            if(health <= 0)
            {
                Death();
            }
        }
    }
    void Death()
    {
        Destroy(gameObject);
        //add more
    }
}

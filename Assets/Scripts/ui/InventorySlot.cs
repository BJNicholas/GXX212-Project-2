using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    //[HideInInspector]
    public bool empty = true;
    public bool full = false;
    [Header("Set Up")]
    public Image icon;
    public Text amountTXT;
    [Header("Stored Item")]
    public GameObject storedItem;
    public int storedAmount;
    public int maxAmount = 100;


    private void Update()
    {
        if (storedItem == null)
        {
            empty = true;
            icon.gameObject.SetActive(false);
            amountTXT.text = "";
        }
        else
        {
            empty = false;
            //print(gameObject.name + " contains " + storedAmount + " " + storedItem.gameObject.name);
            icon.gameObject.SetActive(true);
            icon.sprite = storedItem.GetComponent<Item>().icon;
            amountTXT.text = storedAmount.ToString();
        }

        if(storedAmount <= 0)
        {
            empty = true;
            storedItem = null;
            storedAmount = 0;
        }

        if (storedAmount >= maxAmount)
        {
            full = true;
            CheckOverflow();
        }
        else
        {
            full = false;
        }
    }

    public void CheckOverflow()
    {
        if(storedAmount > maxAmount)
        {
            int dif = storedAmount - maxAmount;
            InventoryManager.instance.AddItem(storedItem, dif);
            storedAmount -= dif;
        }
    }


    private void OnMouseOver()
    {
        //add drag maybe idk man tbh
    }
}

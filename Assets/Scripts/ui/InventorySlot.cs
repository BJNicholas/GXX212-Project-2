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

    private void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(delegate { Clicked(); });
    }

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


    public void Clicked()
    {
        if(InventoryManager.instance.selectedSlot != null)
        {
            GameObject selSlot = InventoryManager.instance.selectedSlot;
            if (selSlot.GetComponent<InventorySlot>().empty)
            {
                InventoryManager.instance.selectedSlot = gameObject;
            }
            else
            {
                if(selSlot.GetComponent<InventorySlot>().storedItem == storedItem && selSlot != gameObject) //therefore this slot is not empty
                {
                    storedAmount += selSlot.GetComponent<InventorySlot>().storedAmount;
                    selSlot.GetComponent<InventorySlot>().storedAmount = 0;
                    CheckOverflow();
                    InventoryManager.instance.selectedSlot = gameObject;
                }
                else
                {
                    if (empty)
                    {
                        storedItem = selSlot.GetComponent<InventorySlot>().storedItem;
                        storedAmount += selSlot.GetComponent<InventorySlot>().storedAmount;

                        selSlot.GetComponent<InventorySlot>().storedItem = null;
                        selSlot.GetComponent<InventorySlot>().storedAmount = 0;
                        InventoryManager.instance.selectedSlot = gameObject;
                    }
                    else
                    {
                        print("Cant put that there");
                        InventoryManager.instance.selectedSlot = gameObject;
                    }
                }
            }
        }
        else
        {
            InventoryManager.instance.selectedSlot = gameObject;
        }
    }
}

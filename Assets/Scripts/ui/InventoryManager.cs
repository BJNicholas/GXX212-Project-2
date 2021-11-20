using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;
    public GameObject[] slots;
    public GameObject selectedSlot = null;
    public List<GameObject> tempStoredItems = null;

    private void Start()
    {
        selectedSlot = null;
        instance = this;
    }

    public void StoreItem(GameObject item, int amount)
    {
        while(amount != 0)
        {
            tempStoredItems.Add(item);
            amount -= 1;
        }
    }

    private void Update()
    {
        if (slots[0].active)
        {
            if(tempStoredItems.ToArray().Length > 0)
            {
                foreach (GameObject item in tempStoredItems)
                {
                    AddItem(item, 1);
                    tempStoredItems.Remove(item);
                    tempStoredItems.TrimExcess();
                }
            }
        }
    }


    public void AddItem(GameObject item, int amount)
    {
        GameObject allocationSlot = null;
        List<GameObject> useableSlots = new List<GameObject>();
        foreach(GameObject slot in slots)
        {
            if(slot.GetComponent<InventorySlot>().storedItem == item && slot.GetComponent<InventorySlot>().full != true)
            {
                allocationSlot = slot;
                break;
            }
            if (slot.GetComponent<InventorySlot>().empty)
            {
                useableSlots.Add(slot);
            }
        }
        if(allocationSlot == null)
        {
            allocationSlot = useableSlots[0];
            allocationSlot.GetComponent<InventorySlot>().storedItem = item;
            allocationSlot.GetComponent<InventorySlot>().storedAmount += amount;
        }
        else
        {
            allocationSlot.GetComponent<InventorySlot>().storedItem = item;
            allocationSlot.GetComponent<InventorySlot>().storedAmount += amount;
        }
        allocationSlot.GetComponent<InventorySlot>().CheckOverflow();
    }
    public void RemoveItem(GameObject item, int amount)
    {
        foreach (GameObject slot in slots)
        {
            if (slot.GetComponent<InventorySlot>().storedItem == item)
            {
                slot.GetComponent<InventorySlot>().storedAmount -= amount;
                break;
            }
        }
    }
}

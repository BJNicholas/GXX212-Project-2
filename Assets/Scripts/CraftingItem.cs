using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CraftingItem : MonoBehaviour
{
    public GameObject item;
    public int amount = 1;
    public Text nameTxt;
    [System.Serializable]
    public class requiredItems
    {
        public GameObject item;
        public int amount;
        [HideInInspector]
        public bool available = false;
    }
    public List<requiredItems> requiredResources = new List<requiredItems>();
    Dictionary<GameObject, int> resourceDict = new Dictionary<GameObject, int>();
    private void Start()
    {
        nameTxt.text = item.name;
        foreach(var resource in requiredResources)
        {
            resourceDict[resource.item] = resource.amount;
        }
    }


    public void CheckAvailability()
    {
        int index = 0;
        while(index != requiredResources.ToArray().Length)
        {
            foreach(GameObject slot in InventoryManager.instance.slots)
            {
                if(slot.GetComponent<InventorySlot>().storedItem == requiredResources[index].item)
                {
                    if(slot.GetComponent<InventorySlot>().storedAmount >= requiredResources[index].amount)
                    {
                        requiredResources[index].available = true;
                        break;
                    }
                    else
                    {
                        requiredResources[index].available = false;
                        break;
                    }
                }
                else
                {
                    requiredResources[index].available = false;
                }
            }
            index += 1;
        }
        int count = 0;
        foreach(requiredItems item in requiredResources)
        {
            if(item.available == true)
            {
                count += 1;
            }
        }
        //its available
        if (requiredResources.ToArray().Length == count)
        {
            Purchase();
            index = 0;
            while (index != requiredResources.ToArray().Length)
            {
                InventoryManager.instance.RemoveItem(requiredResources[index].item, requiredResources[index].amount);
                index += 1;
            }
        }
        else
        {
            GetComponent<Image>().color = Color.red;
            Invoke("Unavailable", 0.1f);
        }
    }
    

    void Unavailable()
    {
        //use this in invoke for 0.1f
        print("Dont have required resources");
        GetComponent<Image>().color = Color.white;
    }


    public void Purchase()
    {
        print("Selected " + item.name);
        if(item.GetComponent<Item>().type == Item.itemTypes.Structures)
        {
            BuildingScript.instance.SpawnPlaceholderStructure(item, item.GetComponentInChildren<MeshRenderer>().sharedMaterial);
            uiManager.instance.CloseMenu(uiManager.instance.inventoryMenu);
        }
        else
        {
            InventoryManager.instance.AddItem(item, amount);
        }
    }
}

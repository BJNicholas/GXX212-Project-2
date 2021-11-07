using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Toolbar : MonoBehaviour
{
    public static Toolbar instance;
    public GameObject hand;
    [Header("INFO")]
    public List<GameObject> slots;
    public int equippedSlot = 0;

    private void Start()
    {
        instance = this;
        UpdateEquippedSlot();
    }

    private void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") != 0f)
        {
            if (Input.GetAxis("Mouse ScrollWheel") < 0f)
            {
                equippedSlot += 1;
                if (equippedSlot >= slots.ToArray().Length)
                {
                    equippedSlot = 0;
                }
            }
            else if (Input.GetAxis("Mouse ScrollWheel") > 0f)
            {
                equippedSlot -= 1;
                if (equippedSlot < 0)
                {
                    equippedSlot = slots.ToArray().Length - 1;
                }
            }
            UpdateEquippedSlot();
        }  
    }

    void UpdateEquippedSlot()
    {
        //All about spawing in tools
        if (hand.transform.childCount > 0)
        {
            Destroy(hand.transform.GetChild(0).gameObject);
        }
        //if slot selected has no item
        if (slots.ToArray()[equippedSlot].GetComponent<InventorySlot>().storedItem == null)
        {

        }
        else
        {
            GameObject newTool = Instantiate(slots.ToArray()[equippedSlot].GetComponent<InventorySlot>().storedItem, hand.transform);
        }


        //updating the ui
        foreach(GameObject slot in slots)
        {
            if(slots.IndexOf(slot) == equippedSlot)
            {
                slot.GetComponent<Image>().color = new Color(slot.GetComponent<Image>().color.r, slot.GetComponent<Image>().color.g, slot.GetComponent<Image>().color.b, 1);

            }
            else
            {
                slot.GetComponent<Image>().color = new Color(slot.GetComponent<Image>().color.r, slot.GetComponent<Image>().color.g, slot.GetComponent<Image>().color.b, 0.5f);
            }
        }
    }

}

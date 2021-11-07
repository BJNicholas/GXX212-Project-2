using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class uiManager : MonoBehaviour
{
    public static uiManager instance;
    public GameObject playerHand;
    public GameObject toolBar;
    public GameObject inventoryMenu;

    private void Start()
    {
        instance = this;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        inventoryMenu.SetActive(false); //Do this for all menus on start
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            OpenMenu(inventoryMenu);
        }
    }

    public void OpenMenu(GameObject menu)
    {
        if (menu.active == true)
        {
            CloseMenu(menu);
        }
        else
        {
            playerHand.SetActive(false);
            toolBar.SetActive(false);
            menu.SetActive(true);
            Camera.main.GetComponent<MouseLook>().enabled = false;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
    }
    public void CloseMenu(GameObject menu)
    {
        playerHand.SetActive(true);
        toolBar.SetActive(true);
        print("CLOSE");
        Camera.main.GetComponent<MouseLook>().enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        menu.SetActive(false);
    }
}

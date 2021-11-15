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
    public GameObject pauseMenu;
    public GameObject ordersMenu;

    private void Start()
    {
        instance = this;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        inventoryMenu.SetActive(false); //Do this for all menus on start
        pauseMenu.SetActive(false);
        ordersMenu.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) OpenMenu(inventoryMenu);
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseMenu.active)
            {
                pauseMenu.SetActive(false);
                Time.timeScale = 1;
                Camera.main.GetComponent<MouseLook>().enabled = true;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else
            {
                pauseMenu.SetActive(true);
                Time.timeScale = 0;
                Camera.main.GetComponent<MouseLook>().enabled = false;
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (ordersMenu.active)
            {
                ordersMenu.SetActive(false);
            }
            else
            {
                ordersMenu.SetActive(true);
            }
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
            toolBar.GetComponent<Toolbar>().enabled = false;
            menu.SetActive(true);
            Camera.main.GetComponent<MouseLook>().enabled = false;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
    }
    public void CloseMenu(GameObject menu)
    {
        playerHand.SetActive(true);
        toolBar.GetComponent<Toolbar>().enabled = true;
        GetComponent<InventoryManager>().selectedSlot = null;
        Camera.main.GetComponent<MouseLook>().enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        menu.SetActive(false);
    }
}

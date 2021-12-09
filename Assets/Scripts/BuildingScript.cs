using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BuildingScript : MonoBehaviour
{
    public static BuildingScript instance;
    //leave selected item and REG null in inspector
    public GameObject selectedItem;
    public Material reg, hologram;

    private void Start()
    {
        instance = this;
    }


    private void Update()
    {
        if(selectedItem != null)
        {
            uiManager.instance.playerHand.SetActive(false);
            uiManager.instance.toolBar.SetActive(false);
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;
            Vector3 point;
            if(Physics.Raycast(ray, out hit))
            {
                point = hit.point;
                selectedItem.transform.position = point;
                float distance = Vector3.Distance(uiManager.instance.playerHand.transform.position, hit.point);
                if (Input.GetMouseButtonDown(0) && distance < 10f)
                {
                    PlaceObject();
                }
            }

            if(Input.GetAxis("Mouse ScrollWheel") != 0f)
            {
                selectedItem.transform.Rotate(Vector3.up * 100 * Input.GetAxis("Mouse ScrollWheel"));
            }
            if(Input.GetMouseButtonDown(1))
            {
                Destroy(selectedItem);
                selectedItem = null;
            }
        }
    }

    public void SpawnPlaceholderStructure(GameObject chosenItem, Material itemMat)
    {
        selectedItem = chosenItem;
        reg = itemMat;
        GameObject structure = Instantiate(selectedItem);
        structure.GetComponentInChildren<MeshRenderer>().material = hologram;
        selectedItem = structure;
        Collider col = structure.GetComponentInChildren(typeof(Collider)) as Collider;
        col.enabled = false;
        if(selectedItem.GetComponent<Rigidbody>() == true)
        {
            selectedItem.GetComponent<Rigidbody>().useGravity = false;
        }
        //nav mesh
        if(selectedItem.GetComponent<NavMeshObstacle>() == true)
        {
            selectedItem.GetComponent<NavMeshObstacle>().enabled = false;
        }
        if (selectedItem.GetComponentInChildren<NavMeshObstacle>() == true)
        {
            selectedItem.GetComponentInChildren<NavMeshObstacle>().enabled = false;
        }
    }
    public void PlaceObject()
    {
        uiManager.instance.playerHand.SetActive(true);
        uiManager.instance.toolBar.SetActive(true);
        selectedItem.GetComponentInChildren<MeshRenderer>().sharedMaterial = reg;
        Collider col = selectedItem.GetComponentInChildren(typeof(Collider)) as Collider;
        col.enabled = true;
        if (selectedItem.GetComponent<Rigidbody>() == true)
        {
            selectedItem.GetComponent<Rigidbody>().useGravity = true;
        }
        //nav mesh
        if (selectedItem.GetComponent<NavMeshObstacle>() == true)
        {
            selectedItem.GetComponent<NavMeshObstacle>().enabled = true;
        }
        if (selectedItem.GetComponentInChildren<NavMeshObstacle>() == true)
        {
            selectedItem.GetComponentInChildren<NavMeshObstacle>().enabled = true;
        }

        selectedItem = null;
        reg = null;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceObjectsOnTerrain : MonoBehaviour
{
    public GameObject[] objectPrefabs; // array must be ordered in the same way as the Tree prototypes in terrain script
    public GameObject Player;
    public float renderDistance = 5;
    TreeInstance[] originalTrees;
    List<TreeInstance> newTrees;
    private void Start()
    {
        TerrainData mapData = gameObject.GetComponent<Terrain>().terrainData;
        originalTrees = mapData.treeInstances;
        newTrees = new List<TreeInstance>(originalTrees);
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        SpawnTreeObjects();
    }

    void SpawnTreeObjects()
    {
        TerrainData mapData = gameObject.GetComponent<Terrain>().terrainData;
        foreach (TreeInstance treeIn in newTrees)
        {
            int objectIndex = treeIn.prototypeIndex;
            Vector3 worldPos = Vector3.Scale(treeIn.position, mapData.size) + Terrain.activeTerrain.transform.position;
            if (Vector3.Distance(worldPos, Player.transform.position) <= renderDistance)
            {
                if (GameObject.Find(worldPos.ToString()))
                {
                    print("object already exists");
                }
                else
                {
                    GameObject newTree = Instantiate(objectPrefabs[objectIndex], worldPos, Quaternion.identity);
                    newTree.name = (Vector3.Scale(treeIn.position, mapData.size) + Terrain.activeTerrain.transform.position).ToString();
                    newTree.transform.SetParent(gameObject.transform);
                    newTrees.Remove(treeIn);
                    mapData.treeInstances = newTrees.ToArray();
                }

            }
        }
    }

    private void OnApplicationQuit()
    {
        GetComponent<Terrain>().terrainData.treeInstances = originalTrees;
    }


}

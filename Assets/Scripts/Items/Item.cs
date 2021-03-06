using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public Sprite icon;

    public enum itemTypes
    {
        Resources,
        Structures,
        Tools,
        Guns
    }
    public itemTypes type;

    private void Start()
    {
        if (type == itemTypes.Structures)
        {
            gameObject.AddComponent<Structure>();
        }
    }
}

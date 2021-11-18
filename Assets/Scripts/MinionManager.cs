using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionManager : MonoBehaviour
{
    public static MinionManager instance;
    public enum states
    {
        idle,
        movingToPoint,
        attacking,
        defending,
        following,
        collectingResources//maybe?
    }
    public List<GameObject> minionsInScene;

    private void Start()
    {
        instance = this;
    }
}

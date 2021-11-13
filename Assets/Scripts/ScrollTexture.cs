using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollTexture : MonoBehaviour
{
    public Vector2 scroll = new Vector2(0.5f,0.5f); // default

    private void Update()
    {
        Vector2 offset;
        offset.x = Time.time * scroll.x;
        offset.y = Time.time * scroll.y;

        GetComponent<Renderer>().material.mainTextureOffset = offset;
    }
}

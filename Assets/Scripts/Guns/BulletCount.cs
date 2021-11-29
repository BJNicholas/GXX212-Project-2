using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletCount : MonoBehaviour
{
    public GunScript gunScript;
    public Text bulletText;

    // Update is called once per frame
    void Update()
    {
       bulletText.text = (gunScript.magAmmo.ToString());
    }
}

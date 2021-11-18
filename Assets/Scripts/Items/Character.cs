using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float health = 100;

    private void Update()
    {
        if (health <= 0)
        {
            if(gameObject.name == "Player Body")
            {
                uiManager.instance.deathScreen.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Die();
            }
        }
    }
    public void Die()
    {
        Destroy(gameObject);
    }
}

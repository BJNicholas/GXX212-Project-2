using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeOfDayMessage : MonoBehaviour
{

    public GameObject timeOfDayPanel;
    public Text messageText;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MessagePopup());
        messageText.text = ("Day 1");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PlayMessage()
    {
        timeOfDayPanel.SetActive(true);
    }

    void CloseMessage()
    {
        timeOfDayPanel.SetActive(false);
    }

    IEnumerator MessagePopup()
    {
        PlayMessage();
        yield return new WaitForSeconds(5f);
        CloseMessage();
    }

}

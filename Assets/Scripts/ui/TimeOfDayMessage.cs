using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeOfDayMessage : MonoBehaviour
{

    public GameObject timeOfDayPanel;
    public Text messageText;
    public LightingManager lightingMngr;

    public bool active = true;
    public bool nightTime = false;

    public int currentDayCount;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MessagePopup());
        currentDayCount = lightingMngr.daycount;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentDayCount<lightingMngr.daycount && nightTime == true)
        {
            StartCoroutine(MessagePopup());
        }

        if(lightingMngr.timeOfDay >= 17 && active == true)
        {
            StartCoroutine(NightTimePopup());
        }
    }

    void PlayMessage()
    {
        timeOfDayPanel.SetActive(true);
        nightTime = false;
        active = true;
        messageText.text = ("Day " + lightingMngr.daycount);
    }

    void CloseMessage()
    {
        timeOfDayPanel.SetActive(false);
    }

    void NightMessage()
    {
        timeOfDayPanel.SetActive(true);
        active = false;
        nightTime = true;
        messageText.text = ("Night is approaching... Get Ready!");
    }

    IEnumerator MessagePopup()
    {
        PlayMessage();
        yield return new WaitForSeconds(5f);
        CloseMessage();
    }
    IEnumerator NightTimePopup()
    {
        NightMessage();
        yield return new WaitForSeconds(5f);
        CloseMessage();
    }
}

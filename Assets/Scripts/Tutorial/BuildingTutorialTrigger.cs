using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingTutorialTrigger : MonoBehaviour
{
    public TutorialScript tutorialScript;

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Friendly")
        {
            tutorialScript.BuildingTutorial();
        }
    }
}

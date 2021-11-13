using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public PostProcessVolume volume;
    //Sliders
    public Slider sensitivity;
    public Slider fov;
    //Audio sub-section
    public Slider music;
    public Slider effects;
    //toggles
    public MotionBlur motionBlur = null;
    //dropdowns
    public Dropdown aa;

    private void Start()
    {
        sensitivity.value = MouseLook.instance.mouseSensitivity;
        fov.value = Camera.main.fieldOfView;

        volume.profile.TryGetSettings<MotionBlur>(out motionBlur);
    }

    private void Update()
    {
        MouseLook.instance.mouseSensitivity = sensitivity.value;
        Camera.main.fieldOfView = fov.value;
        // all pp stuff under here
        aa.onValueChanged.AddListener(delegate { AADropdownItemSelected(aa); });

    }

    void AADropdownItemSelected(Dropdown dropdown)
    {
        int index = dropdown.value;
        Camera.main.gameObject.GetComponent<PostProcessLayer>().antialiasingMode = (PostProcessLayer.Antialiasing)index;

    }

    public void MotionBlurToggle(Toggle tglValue)
    {

        if (motionBlur.active == true) motionBlur.active = false;
        else motionBlur.active = true;
    }



    public void ReturnToPauseMenu()
    {
        pauseMenu.SetActive(true);
        gameObject.SetActive(false);
    }
}

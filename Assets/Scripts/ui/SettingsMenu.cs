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
    public List<GameObject> audioEffectObjects; // Must be filled on awake/ before start
    //toggles
    public MotionBlur motionBlur = null;
    public AmbientOcclusion ambientOcc = null;
    //dropdowns
    public Dropdown aa;

    private void Start()
    {
        sensitivity.value = MouseLook.instance.mouseSensitivity;
        fov.value = Camera.main.fieldOfView;
        music.maxValue = GameObject.Find("MUSIC").GetComponent<AudioSource>().volume;
        effects.maxValue = 0;
        foreach(GameObject audioEffect in audioEffectObjects)
        {
            if(audioEffect.GetComponent<AudioSource>().volume > effects.maxValue)
            {
                effects.maxValue = audioEffect.GetComponent<AudioSource>().volume;
            }
        }

        volume.profile.TryGetSettings<MotionBlur>(out motionBlur);
        volume.profile.TryGetSettings<AmbientOcclusion>(out ambientOcc);
    }

    private void Update()
    {
        GameObject.Find("MUSIC").GetComponent<AudioSource>().volume = music.value;
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
    public void AmbientOcclusionToggle(Toggle tglValue)
    {

        if (ambientOcc.active == true) ambientOcc.active = false;
        else ambientOcc.active = true;
    }



    public void ReturnToPauseMenu()
    {
        pauseMenu.SetActive(true);
        gameObject.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteAlways]
public class LightingManager : MonoBehaviour
{
    public Light directionalLight;
    public LightingPreset preset;
    public float daySpeed;
    public int daycount;
    [Range(0, 24)] public float timeOfDay;

    bool newDay;
    private void Update()
    {
        if(preset == null) return;

        if (Application.isPlaying)
        {
            timeOfDay += daySpeed * Time.deltaTime;
            timeOfDay %= 24; //clamp between 0-24
            UpdateLighting(timeOfDay / 24f);

            if ((timeOfDay > 5.9 && timeOfDay < 6) && newDay == false)
            {
                daycount += 1;
                GameObject[] spawners = GameObject.FindGameObjectsWithTag("Spawner");
                foreach (GameObject spawner in spawners) spawner.GetComponent<ZombieSpawner>().maxPerDay = 20 * daycount;
                newDay = true;
            }
            else if (timeOfDay > 6) newDay = false;
        }
        else
        {
            UpdateLighting(timeOfDay / 24f);
        }
    }

    void UpdateLighting(float timePercent)
    {
        RenderSettings.ambientLight = preset.ambientColour.Evaluate(timePercent);
        RenderSettings.fogColor = preset.fogColour.Evaluate(timePercent);
        
        if(directionalLight != null)
        {
            directionalLight.color = preset.directionalColour.Evaluate(timePercent);
            directionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) - 90f, -170, 0));
        }
    }

    private void OnValidate()
    {
        if (directionalLight != null) return;
        if (RenderSettings.sun != null) directionalLight = RenderSettings.sun;
        else
        {
            Light[] lights = GameObject.FindObjectsOfType<Light>();
            foreach(Light light in lights)
            {
                if(light.type == LightType.Directional)
                {
                    directionalLight = light;
                    return;
                }
            }
        }
    }
}
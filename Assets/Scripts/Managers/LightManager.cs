using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[ExecuteAlways]
public class LightManager : MonoBehaviour
{
    public bool pauseDaylightCycle = false;

    // References
    public Light DirectionalLight;

    // Colours
    public Gradient FogGradient;

    // Variables
    [Range(0, 24)] 
    public float TimeOfDay;

    [Range(0, 24)]
    public float MorningHour = 6f;
    [Range(0, 24)]
    public float EveningHour = 18f;

    // Events
    public UnityEvent IsDaytime;
    public UnityEvent IsNighttime;

    [SerializeField] private bool IsDay;
    [SerializeField] private bool IsNight;
    public float SecondsInAnHour = 10f;// 10 makes day night/cycle 4 minutes | 60 makes a day/night cycle 24 minutes | 3600 makes a day/night cycle take 24 hours

    [SerializeField] private float MinutesPerDay;

    private void Start()
    {
        TimeOfDay = 0f;
    }
    private void Update()
    {
        if (Application.isPlaying)
        {
            if (!pauseDaylightCycle)
            {
                TimeOfDay += Time.deltaTime * (SecondsInAnHour / 100);
            }
            TimeOfDay %= 24; // Clamp between 0 and 24
            UpdateLighting(TimeOfDay / 24f);
            MinutesPerDay = ((SecondsInAnHour * 24) / 60);


            Color currentFogColour = FogGradient.Evaluate(TimeOfDay / 24);
            RenderSettings.fogColor = currentFogColour;

        }
        else
        {
            UpdateLighting(TimeOfDay / 24f);
            MinutesPerDay = ((SecondsInAnHour * 24) / 60);
        }

        if (MorningHour < TimeOfDay && TimeOfDay < EveningHour)// 6am and 6pm | Daytime Check
        {
            IsDay = true;
            IsNight = false;
            IsDaytime.Invoke();
        }
        else if (TimeOfDay < MorningHour)
        {
            IsDay = false;
            IsNight = true;
            IsNighttime.Invoke();
        }
        else if (TimeOfDay > EveningHour)
        {
            IsDay = false;
            IsNight = true;
            IsNighttime.Invoke();
        }

    }
    private void UpdateLighting(float timePercent)
    {
        if(DirectionalLight != null)
        {
            DirectionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) - 90f, 120f, -100f));//84
        }
    }


    private void OnValidate()
    {
        if (DirectionalLight != null)
        {
            return;
        }

        if (RenderSettings.sun != null)
        {
            DirectionalLight = RenderSettings.sun;
        }
        else
        {
            Light[] lights = GameObject.FindObjectsOfType<Light>();
            foreach (Light light in lights)
            {
                if(light.type == LightType.Directional)
                {
                    DirectionalLight = light;
                }
            }
        }
    }

}

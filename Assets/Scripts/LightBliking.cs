using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBliking : MonoBehaviour
{
    public Light lightComponent;
    public float blinkingSpeed;
    public float minIntensity;
    public float maxIntensity;

    void Start()
    {
        lightComponent = GetComponent<Light>();
    }

    void Update()
    {
        float intensity = Mathf.Lerp(minIntensity, maxIntensity, Mathf.Sin(Time.time * blinkingSpeed) * 0.5f + 0.5f);
        lightComponent.intensity = intensity;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightsFlicker : MonoBehaviour
{
    public float flickerIntensity = 0.7f;
    public float flickerPerSecond = 3.0f;
    public float speedRandomness = 1.0f;

    private float time;
    private float startingIntensity;
    private Light lighterLight;

    void Start()
    {
        lighterLight = GetComponent<Light>();
        startingIntensity = lighterLight.intensity;
    }

    void Update()
    {
        time += Time.deltaTime * (1 - Random.Range(-speedRandomness, speedRandomness)) * Mathf.PI;
        lighterLight.intensity = startingIntensity + Mathf.Sin(time * flickerPerSecond) * flickerIntensity;
    }
}

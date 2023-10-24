using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwappingLightmaps : MonoBehaviour
{
    public Light candleLight1;
    public ParticleSystem candleFlame1;

    public Texture2D[] darkLightmapDir, darkLightmapColor;
    public Texture2D[] brightLightmapDir, brightLightmapColor;

    public LightmapData[] darkLightmap, brightLightmap;

    private bool lightIsOn = false;

    void Start()
    {
        List<LightmapData> dlightmap = new List<LightmapData>();

        for (int i = 0; i < darkLightmapDir.Length; i++)
        {
            LightmapData lmdata = new LightmapData();

            lmdata.lightmapDir = darkLightmapDir[i];
            lmdata.lightmapColor = darkLightmapColor[i];

            dlightmap.Add(lmdata);
        }

        darkLightmap = dlightmap.ToArray();

        List<LightmapData> blightmap = new List<LightmapData>();

        for (int i = 0; i < brightLightmapDir.Length; i++)
        {
            LightmapData lmdata = new LightmapData();

            lmdata.lightmapDir = brightLightmapDir[i];
            lmdata.lightmapColor = brightLightmapColor[i];

            blightmap.Add(lmdata);
        }

        brightLightmap = blightmap.ToArray();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (lightIsOn)
            {
                candleLight1.enabled = true;
                candleFlame1.Play();
                LightmapSettings.lightmaps = brightLightmap;
                lightIsOn = false;
            }
            else
            {
                candleLight1.enabled = false;
                candleFlame1.Stop();
                LightmapSettings.lightmaps = darkLightmap;
                lightIsOn = true;
            }

        }
    }

    public void TurnOnLights()
    {
        if (lightIsOn)
        {
            //candle_1.SetActive(true);
            LightmapSettings.lightmaps = brightLightmap;
            lightIsOn = false;
        }
        else
        {
            //candle_1.SetActive(false);
            LightmapSettings.lightmaps = darkLightmap;
            lightIsOn = true;
        }
    }

    void CreateFlame()
    {
        candleFlame1.Play();
    }
}


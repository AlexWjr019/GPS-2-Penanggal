using System.Collections.Generic;
using UnityEngine;

public class InteractCandle : MonoBehaviour
{
    public GameObject[] candles;
    public bool[] isOn;
    public Light[] candleLights;
    public ParticleSystem[] particleSystems;

    public Texture2D[] darkLightmapDir, darkLightmapColor;
    public Texture2D[] brightLightmapDir, brightLightmapColor;

    public LightmapData[] darkLightmap, brightLightmap;

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
        /*if (candles[0] && !isOn[0])
        {
            candleLights[0].enabled = false;
            particleSystems[0].Stop();
            //cabinetAnimator.SetBool("isOpen", false);
        }
        else
        {
            candleLights[0].enabled = true;
            particleSystems[0].Play();
            //cabinetAnimator.SetBool("isOpen", true);
        }*/

        if (candles[0] && !isOn[0])
        {
            candleLights[0].enabled = false;
            //particleSystems[0].Stop();
            LightmapSettings.lightmaps = darkLightmap;
            //isOn[0] = true;
            Debug.Log("dark");
        }
        else
        {
            candleLights[0].enabled = true;
            //particleSystems[0].Play();
            LightmapSettings.lightmaps = brightLightmap;
            //isOn[0] = false;
            Debug.Log("bright");
        }
    }
}

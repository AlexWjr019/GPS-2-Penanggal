using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwappingLightmaps : MonoBehaviour
{
    public Texture2D[] darkLightmapDir, darkLightmapColor;
    public Texture2D[] brightLightmapDir, brightLightmapColor;

    public LightmapData[] darkLightmap, brightLightmap;

    public  bool testing = false;

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

        LightmapSettings.lightmaps = brightLightmap;
    }

    void Update()
    {
        if (testing)
        {
            LightmapSettings.lightmaps = darkLightmap;
        }
    }
}


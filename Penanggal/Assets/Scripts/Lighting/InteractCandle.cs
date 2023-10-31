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
        isOn[0] = true;
    }

    // Removed Update method since touch handling is moved to another script

    public void ToggleCandle(int candleIndex)
    {
        if (!isOn[candleIndex])
        {
            string selectedItemName = InventoryManager.Instance.GetSelectedInventoryItemName();

            if (selectedItemName == "Lighter")
            {
                candleLights[candleIndex].enabled = true;
                LightmapSettings.lightmaps = brightLightmap;
                isOn[candleIndex] = true;
                Debug.Log("bright");
            }
        }
        else
        {
            candleLights[candleIndex].enabled = false;
            LightmapSettings.lightmaps = darkLightmap;
            isOn[candleIndex] = false;
            Debug.Log("dark");
        }
    }
}

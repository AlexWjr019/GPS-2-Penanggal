using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candle : MonoBehaviour, ItemInventory
{
    public string Name
    {
        get
        {
            return "Candle";
        }
    }

    public Sprite image = null;

    public Sprite Image
    {
        get
        {
            return image;
        }

    }

    public void OnPickup()
    {
        gameObject.SetActive(false);
    }
}

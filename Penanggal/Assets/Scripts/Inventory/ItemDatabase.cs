using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public static ItemDatabase Instance;

    [System.Serializable]
    public struct ItemPrefabPair
    {
        public string itemName;
        public GameObject itemPrefab;
    }

    public ItemPrefabPair[] items;
    private Dictionary<string, GameObject> itemPrefabDict = new Dictionary<string, GameObject>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        foreach (var pair in items)
        {
            itemPrefabDict.Add(pair.itemName, pair.itemPrefab);
        }
    }

    public GameObject GetPrefab(string itemName)
    {
        if (itemPrefabDict.TryGetValue(itemName, out GameObject prefab))
        {
            return prefab;
        }
        else
        {
            Debug.LogError($"No prefab found for item: {itemName}");
            return null;
        }
    }
}


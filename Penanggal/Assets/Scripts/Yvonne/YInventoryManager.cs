using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class YInventoryManager : MonoBehaviour
{
    public static YInventoryManager Instance;
    public List<Item> Items = new List<Item>();

    public Transform ItemContent;
    public GameObject InventoryItem;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        // clean content before open
        foreach (Transform item in ItemContent)
        {
            Destroy(item.gameObject);
        }

        foreach (var item in Items)
        {
            GameObject obj = Instantiate(InventoryItem, ItemContent);
            var itemName = obj.transform.Find("Item/ItemName").GetComponent<TMP_Text>();
            var itemIcon = obj.transform.Find("Item/ItemIcon").GetComponent<Image>();

            itemName.text = item.itemName;
            itemIcon.sprite = item.itemIcon;
        }
    }

    public void Add(Item item)
    {
        Items.Add(item);

        /*foreach (var itemz in Items)
        {
            GameObject obj = Instantiate(InventoryItem, ItemContent);
            var itemName = obj.transform.Find("ItemName").GetComponent<TMP_Text>();
            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();

            itemName.text = itemz.itemName;
            itemIcon.sprite = itemz.itemIcon;
        }*/
    }

    public void ListItems()
    {
        /*// clean content before open
        foreach (Transform item in ItemContent)
        {
            Destroy(item.gameObject);
        }

        foreach (var item in Items)
        {
            GameObject obj = Instantiate(InventoryItem, ItemContent);
            var itemName = obj.transform.Find("Items/ItemName").GetComponent<TMP_Text>();
            var itemIcon = obj.transform.Find("Items/ItemIcon").GetComponent<Image>();

            itemName.text = item.itemName;
            itemIcon.sprite = item.itemIcon;
        }*/
    }
}

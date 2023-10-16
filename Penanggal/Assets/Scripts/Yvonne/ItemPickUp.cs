using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public Item Item;

    void Start()
    {
        //YInventoryManager.Instance.ListItems();
    }

    void Pickup()
    {
        YInventoryManager.Instance.Add(Item);
        Debug.Log("added");
        Destroy(gameObject);
    }

    private void OnMouseDown()
    {
        Pickup();
    }
}

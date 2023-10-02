using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject interactButton;
    public InventorySlot[] inventorySlots;
    public GameObject inventoryItemPrefab;

    public static InventoryManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (interactButton != null)
        {
            interactButton.SetActive(false);
        }
    }

    public InventorySlot GetEmptySlot()
    {
        foreach (InventorySlot slot in inventorySlots)
        {
            if (slot.transform.childCount == 0)
            {
                return slot;
            }
        }
        return null;
    }
}

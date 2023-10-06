using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject interactButton;
    public InventorySlot[] inventorySlots;
    public GameObject inventoryItemPrefab;

    int selectedSlot = -1;
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
        ChangeSelectedSlot(0);
        if (interactButton != null)
        {
            interactButton.SetActive(false);
        }
    }

    private void Update()
    {
        if (inventorySlots[0].isSelected)
        {
            inventorySlots[1].isSelected = false;
        }
        if (inventorySlots[1].isSelected)
        {
            inventorySlots[0].isSelected = false;
        }
    }

    public string GetSelectedInventoryItemName()
    {
        foreach (InventorySlot slot in inventorySlots)
        {
            if (slot.isSelected)
            {
                InventoryItems item = slot.GetCurrentItem();
                if (item != null)
                {
                    return item.name;
                }
            }
        }
        return null;
    }


    public void DestroySelectedItem()
    {
        foreach (InventorySlot slot in inventorySlots)
        {
            if (slot.isSelected)
            {
                InventoryItems item = slot.GetCurrentItem();
                if (item != null)
                {
                    Destroy(item.gameObject);
                }
            }
        }
    }

    void ChangeSelectedSlot(int newValue)
    {
        if (selectedSlot >= 0)
        {
            inventorySlots[selectedSlot].Deselected();
        }
        inventorySlots[newValue].Selected();
        selectedSlot = newValue;
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

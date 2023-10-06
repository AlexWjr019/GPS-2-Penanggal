using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject interactButton;
    public InventorySlot[] inventorySlots;
    public GameObject inventoryItemPrefab;

    //int selectedSlot = -1;
    public static InventoryManager Instance { get; private set; }
    public InventorySlot selectedSlot;

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
        //ChangeSelectedSlot(0);
        if (interactButton != null)
        {
            interactButton.SetActive(false);
        }
    }

    private void Update()
    {
        foreach (InventorySlot slot in inventorySlots)
        {
            if (slot != selectedSlot && slot.isSelected)
            {
                slot.Deselected();
            }
        }
    }

    public void UpdateSelectedSlot(InventorySlot newSelectedSlot)
    {
        if (selectedSlot != null)
        {
            selectedSlot.Deselected();
        }

        selectedSlot = newSelectedSlot;

        if (selectedSlot != null)
        {
            selectedSlot.Selected();
        }
    }

    public InventorySlot GetSlotToReplace()
    {
        return selectedSlot;
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

    public bool IsInventoryFull()
    {
        foreach (var slot in inventorySlots)
        {
            if (slot.IsEmpty())
            {
                return false;
            }
        }
        return true;
    }


    public void SwapItems(InventorySlot slot, InventoryItems newItem, Vector3 dropPosition)
    {
        InventoryItems currentItem = slot.GetCurrentItem();
        if (currentItem != null)
        {
            Instantiate(currentItem.itemPrefab3D, dropPosition, Quaternion.identity);
            Destroy(currentItem.gameObject);
        }

        newItem.transform.SetParent(slot.transform);
        newItem.transform.localPosition = Vector3.zero;
        newItem.transform.localScale = Vector3.one;
    }

}

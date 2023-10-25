using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour /*, IDropHandler*/, IPointerClickHandler
{
    public Image image;
    public Color selecterColor, notSelectedColor;

    public bool isSelected = false;
    private void Awake()
    {
        Deselected();
    }

    private void Update()
    {
        if (isSelected)
        {
            Selected();
        } else
        {
            Deselected();
        }
        //Debug.Log(isSelected);
    }
    public void Selected()
    {
        isSelected = true;
        Color newColor = selecterColor;
        newColor.a = 1.0f;
        image.color = newColor;
    }

    public void Deselected()
    {
        isSelected = false;
        Color newColor = notSelectedColor;
        newColor.a = 1.0f;
        image.color = newColor;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isSelected)
        {
            Deselected();
            InventoryManager.Instance.UpdateSelectedSlot(null);
        }
        else
        {
            Selected();
            InventoryManager.Instance.UpdateSelectedSlot(this);
        }
    }
    //public void OnDrop(PointerEventData eventData)
    //{
    //    InventoryItems draggedItem = eventData.pointerDrag.GetComponent<InventoryItems>();
    //    if (draggedItem != null)
    //    {
    //        if (transform.childCount == 0)
    //        {
    //            draggedItem.parentAfterDrag = transform;
    //        }
    //        else
    //        {
    //            // Using SwapItems method from InventoryManager
    //            InventoryManager.Instance.SwapItems(this, draggedItem, draggedItem.transform.position);
    //        }
    //    }
    //}


    public string GetCurrentItemName()
    {
        if (transform.childCount > 0)
        {
            InventoryItems item = transform.GetChild(0).GetComponent<InventoryItems>();
            if (item != null)
            {
                return item.itemName;
            }
        }
        return null;
    }

    public InventoryItems GetCurrentItem()
    {
        if (transform.childCount > 0)
        {
            InventoryItems item = transform.GetChild(0).GetComponent<InventoryItems>();
            if (item != null)
            {
                return item;
            }
        }
        return null;
    }

    public bool IsEmpty()
    {
        return transform.childCount == 0;
    }

    public void ClearSlot()
    {
        if (transform.childCount > 0)
        {
            Destroy(transform.GetChild(0).gameObject);
        }
        Deselected();
    }

    public void SwapItem(InventoryItems newItem)
    {
        InteractionSystem interactionSystem = FindObjectOfType<InteractionSystem>();

        InventoryItems currentItem = transform.GetChild(0).GetComponent<InventoryItems>();

        currentItem.transform.SetParent(null);
        newItem.transform.SetParent(transform);
        newItem.transform.localPosition = Vector3.zero;
        newItem.transform.localScale = Vector3.one;

        GameObject droppedItem3D = Instantiate(currentItem.itemPrefab3D, interactionSystem.LastInteractedItemPosition(), Quaternion.identity);

        Destroy(currentItem.gameObject);
    }

}

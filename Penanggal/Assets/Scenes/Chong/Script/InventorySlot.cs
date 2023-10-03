using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour , IDropHandler, IPointerClickHandler
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
        }
        else
        {
            Selected();
        }
    }
    public void OnDrop(PointerEventData eventData)
    {
        if(transform.childCount == 0)
        {
            InventoryItems inventoryItems = eventData.pointerDrag.GetComponent<InventoryItems>();
            inventoryItems.parentAfterDrag = transform;
        }
    }
}

  using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDrop : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        RectTransform invPanel = transform as RectTransform;

        if(!RectTransformUtility.RectangleContainsScreenPoint(invPanel, Input.mousePosition))
        {
            // use touch input
            Debug.Log("Drop item");
        }
    }
}

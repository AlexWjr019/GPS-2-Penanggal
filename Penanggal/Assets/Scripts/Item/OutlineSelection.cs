using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OutlineSelection : MonoBehaviour
{
    private ItemOutline outlineScript;

    private void Start()
    {
        outlineScript = GetComponent<ItemOutline>();
        // Disable the ItemOutline script at the start.
        outlineScript.enabled = false;
    }

    private void OnMouseEnter()
    {
        // When the mouse pointer enters the object, enable the ItemOutline script.
        outlineScript.enabled = true;
    }

    private void OnMouseExit()
    {
        // When the mouse pointer exits the object, disable the ItemOutline script.
        outlineScript.enabled = false;
    }
}

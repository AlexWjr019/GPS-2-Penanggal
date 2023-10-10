using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OpenSafeCode : MonoBehaviour
{
    public GameObject safeCodeWheel;
    //public Safecode safeCodeScript;

    //For Mouse Testing
    private void OnMouseDown()
    {
        // Check if the canvas exists and is not active
        if (safeCodeWheel && !safeCodeWheel.activeSelf  && !LockControl.safeIsOpen/*&&*/
            /*safeCodeScript != null*/ /*&& safeCodeScript.UiText.text != "Successful"*/)
        {
            // Activate the canvas
            safeCodeWheel.SetActive(true);
        }
        //if (safeCodeScript == null)
        //{
        //    Debug.LogError("Safecode script is null!");
        //}
    }

    //Mobile Pointer
    public void OnPointerClick(PointerEventData eventData)
    {
        // Check if the canvas exists, is not active, and Safecode's UiText is not "Successful"
        if (safeCodeWheel && !safeCodeWheel.activeSelf && !LockControl.safeIsOpen/*&&*/
            /*safeCodeScript != null*/ /*&& safeCodeScript.UiText.text != "Successful"*/)
        {
            // Activate the canvas
            safeCodeWheel.SetActive(true);
        }
    }
}

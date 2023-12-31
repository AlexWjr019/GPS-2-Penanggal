using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OpenSafeCode : MonoBehaviour
{
    public GameObject safeCodeWheel /*oriSafeCodeWheel*/;
    //public GameObject ObjectCamera;
    public static bool openSafe;
    public GameObject playerHead;
    private bool objectiveActive = true;
    public GameObject pauseButton;

    //For Mouse Testing
    private void OnMouseDown()
    {
        // Check if the canvas exists and is not active
        if (safeCodeWheel && !safeCodeWheel.activeSelf  && !LockControl.safeIsOpen/*&&*/
            /*safeCodeScript != null*/ /*&& safeCodeScript.UiText.text != "Successful"*/)
        {
            // Activate the canvas
            //oriSafeCodeWheel.SetActive(false);
            safeCodeWheel.SetActive(true);
            pauseButton.SetActive(false);
            playerHead.SetActive(false);
            openSafe = true;
            Debug.Log("OnMouseDown triggered. Setting cursor state.");
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            if (objectiveActive)
            {
                ObjectiveManager.objective = true;
                objectiveActive = false;
            }
            
            //ObjectCamera.SetActive(true);
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
            pauseButton.SetActive(false);
            playerHead.SetActive(false);
            openSafe = true;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectInteract : MonoBehaviour
{
    public GameObject standFrame;
    public GameObject playerCamera;
    public static bool picIsRotate;

    private void OnMouseDown()
    {
        standFrame.SetActive(true);
        playerCamera.SetActive(false);
        picIsRotate = true;
    }

    public void Return()
    {
        playerCamera.SetActive(true);
        picIsRotate = false;
    }
}

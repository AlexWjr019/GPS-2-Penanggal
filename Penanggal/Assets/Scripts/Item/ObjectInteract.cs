using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectInteract : MonoBehaviour
{
    public GameObject standFrame;
    public GameObject playerCamera;
    public GameObject pauseButton;
    public static bool picIsRotate;

    private void OnMouseDown()
    {
        standFrame.SetActive(true);
        playerCamera.SetActive(false);
        pauseButton.SetActive(false);
        picIsRotate = true;
    }

    public void Return()
    {
        playerCamera.SetActive(true);
        pauseButton.SetActive(true);
        picIsRotate = false;
    }
}

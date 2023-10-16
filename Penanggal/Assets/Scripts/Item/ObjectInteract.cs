using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectInteract : MonoBehaviour
{
    public GameObject objectCamera;
    public GameObject standFrame, oriStandFrame;
    public bool frame;

    private void OnMouseDown()
    {
        if (frame)
        {
            objectCamera.SetActive(true);
            standFrame.SetActive(true);
            oriStandFrame.SetActive(false);
        }

    }
}

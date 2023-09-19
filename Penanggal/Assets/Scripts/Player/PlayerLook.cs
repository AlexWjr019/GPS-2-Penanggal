/*using System;
using UnityEngine;

[Serializable]
public class PlayerLook
{
    public float sensitivityX = 2f;
    public float sensitivityY = 2f;
    public bool clampVerticalRotation = true;
    public float minX = -90f;
    public float maxX = 90f;
    public bool smooth;
    public float smoothTime = 5f;
    public bool lockCursor = true;

    private Quaternion 
}*/

/*using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public FixedTouchField TouchField;

    public float cameraSensitivity = 100f;
    public float lookXLimit = 45.0f;
    public Camera playerCamera;

    public Transform playerBody;

    float xRotation = 0f;

    [HideInInspector]
    public Vector2 RunAxis;
    [HideInInspector]
    public Vector2 LookAxis;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        *//*float mouseX = Input.GetAxis("Mouse X") * cameraSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * cameraSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);*/

        /*xRotation += -LookAxis.y * cameraSensitivity;
        xRotation = Mathf.Clamp(xRotation, -lookXLimit, lookXLimit);
        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        transform.rotation *= Quaternion.Euler(0, LookAxis.x * cameraSensitivity, 0);*/

        /*float mouseX = 0;
        float mouseY = 0;

        if(Input.touchCount > 0 && (Input.GetTouch(0).phase == TouchPhase.Moved))
        {
            mouseX = Input.GetTouch(0).deltaPosition.x;
            mouseY = Input.GetTouch(0).deltaPosition.y;
        }

        mouseX *= cameraSensitivity;
        mouseY *= cameraSensitivity;

        xRotation -= mouseY * Time.deltaTime;
        xRotation = Mathf.Clamp(xRotation, -80, 80);

        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        playerBody.Rotate(Vector3.up * mouseX * Time.deltaTime);*//*

        var fps = GetComponent<FirstPersonController>();
        //fps..LookAxis = TouchField.TouchDist;

    }
}
*/
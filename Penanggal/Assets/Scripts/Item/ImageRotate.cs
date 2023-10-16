using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ImageRotate : MonoBehaviour
{
    [SerializeField] private InputAction pressed, axis;

    [SerializeField] private float rotateSpeed = 1.0f;

    private Vector2 rotation;

    private bool rotateAllowed;

    private void Awake()
    {
        pressed.Enable();
        axis.Enable();
    }

    private void Update()
    {
        pressed.performed += _ => { StartCoroutine(Rotate()); };
        pressed.canceled += _ => { rotateAllowed = false; };
        axis.performed += context => { rotation = context.ReadValue<Vector2>(); };
    }

    private IEnumerator Rotate()
    {
        rotateAllowed = true;
        while (rotateAllowed)
        {
            //apply rotation
            rotation *= rotateSpeed;
            transform.Rotate(Vector3.up, rotation.x, Space.World);
            transform.Rotate(Vector3.right, rotation.y, Space.World);
            yield return null;
        }
    }

    public void ResetImageRotation()
    {
        transform.rotation = Quaternion.Euler(-90f, 0f, 0f);
    }

}

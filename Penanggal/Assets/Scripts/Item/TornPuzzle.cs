using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class TornPuzzle : MonoBehaviour
{
    private Vector3 originalPosition;
    private Vector3 screenPoint;
    private Vector3 offset;
    private Vector3 initialMousePosition;
    public static bool isDragging = false;
    private bool isColliding = false;
    private GameObject collidingObject;
    private bool isRotating = false;
    public GameObject pic0, pic1, pic2, pic3, pic4, pic5, pic6, pic7, pic8;
    private float lastClickTime;
    public float doubleClickTimeThreshold = 0.3f;
    public static bool getPinPaper = false;
    public static bool isInteraction = false;

    private List<GameObject> collidingObjects = new List<GameObject>();

    public GameObject wholePicture, picPuzzle; // The whole picture object
    private bool callBaby = false;
    private bool isCalled = false;

    //[SerializeField]
    //private BabySpawner babySpawner;

    private void Start()
    {
        originalPosition = transform.position;
    }

    private void Update()
    {
        if (isRotating)
        {
            // Rotate the object 90 degrees clockwise
            transform.Rotate(Vector3.forward * 90f);
            isRotating = false;
        }
        Debug.Log(isInteraction);

        CallBabyPenanggal();
    }

    private void OnMouseDown()
    {
        if (/*isInteraction &&*/ gameObject.CompareTag("Swap"))
        {
            float timeSinceLastClick = Time.time - lastClickTime;

            if (timeSinceLastClick <= doubleClickTimeThreshold)
            {
                // Double click detected, trigger rotation
                isRotating = true;
            }
            else
            {
                // Single click, start dragging
                initialMousePosition = Input.mousePosition;
                screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
                offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(initialMousePosition.x, initialMousePosition.y, screenPoint.z));
                isDragging = true;
            }

            lastClickTime = Time.time;
            callBaby = true;
        }
        
    }

    public void CallBabyPenanggal()
    {
        if (callBaby) 
        {
            if (!isCalled)
            {
                Debug.Log("call baby" + callBaby);
                BabySpawner.Instance.spawnBaby = true;
                BabySpawner.Instance.StartCoroutine(BabySpawner.Instance.SpawnBaby());
                isCalled = true;
            }
        }
    }

    private void OnMouseDrag()
    {
        if (isDragging)
        {
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
            transform.position = curPosition;
        }
    }

    private void OnMouseUp()
    {
        if (isRotating)
        {
            // Rotate the object 90 degrees clockwise when clicked
            transform.Rotate(Vector3.forward * 90f);
        }
        else if (isDragging)
        {
            // Handle dragging actions here
            if (isColliding && collidingObject != null && gameObject.CompareTag("Swap") && collidingObject.CompareTag("Swap"))
            {
                SwapPositions(collidingObject);
            }
            else
            {
                // Snap the object back to its original position
                transform.position = originalPosition;
            }
            CheckOrder();
        }

        isDragging = false;
        isRotating = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        isColliding = true;
        collidingObject = other.gameObject;
    }

    private void OnTriggerExit(Collider other)
    {
        isColliding = false;
        collidingObject = null;
    }

    private void SwapPositions(GameObject otherObject)
    {
        // Store the original positions of both objects
        Vector3 otherOriginalPosition = otherObject.GetComponent<TornPuzzle>().originalPosition;

        // Swap the objects' positions
        otherObject.transform.position = originalPosition;
        transform.position = otherOriginalPosition;

        // Update the original positions
        originalPosition = transform.position;
        otherObject.GetComponent<TornPuzzle>().originalPosition = otherObject.transform.position;
    }

    private void CheckOrder()
    {
        if (pic0.transform.position.x > pic1.transform.position.x && pic1.transform.position.x > pic2.transform.position.x &&
        pic3.transform.position.x > pic4.transform.position.x && pic4.transform.position.x > pic5.transform.position.x &&
        pic6.transform.position.x > pic7.transform.position.x && pic7.transform.position.x > pic8.transform.position.x &&
        pic0.transform.position.y > pic3.transform.position.y && pic3.transform.position.y > pic6.transform.position.y &&
        pic1.transform.position.y > pic4.transform.position.y && pic4.transform.position.y > pic7.transform.position.y &&
        pic2.transform.position.y > pic5.transform.position.y && pic5.transform.position.y > pic8.transform.position.y)
        {
            Debug.Log("Correct Order");
            isDragging = false;
            isRotating = false;
            callBaby = false;
            BabySpawner.Instance.spawnBaby = false;
            BabySpawner.Instance.StopCoroutine(BabySpawner.Instance.SpawnBaby());
            CreateWholePicture(); // Call the method to create the whole picture
        }
    }

    private void CreateWholePicture()
    {
        // Create the whole picture by enabling it
        wholePicture.SetActive(true);
        getPinPaper = true;
        picPuzzle.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swap : MonoBehaviour
{
    private Vector3 originalPosition;
    private Vector3 screenPoint;
    private Vector3 offset;
    private bool isDragging = false;
    private bool isColliding = false;
    private GameObject collidingObject;

    public GameObject bowl;
    public GameObject cup;
    public GameObject box;
    private List<GameObject> collidingObjects = new List<GameObject>();
    public static bool weddingPuzzle = false;

    private void Start()
    {
        originalPosition = transform.position;
    }

    private void OnMouseDown()
    {
        if (gameObject.CompareTag("Swap"))
        {
            screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
            offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
            isDragging = true;
        }
    }

    private void OnMouseDrag()
    {
        if (isDragging)
        {
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
            transform.position = curPosition;
            weddingPuzzle = true;
        }
    }

    private void OnMouseUp()
    {
        isDragging = false;
        weddingPuzzle = false;

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
        Vector3 tempOriginalPosition = originalPosition;
        Vector3 otherOriginalPosition = otherObject.GetComponent<Swap>().originalPosition;

        // Swap the objects' positions
        otherObject.transform.position = tempOriginalPosition;
        transform.position = otherOriginalPosition;

        // Update the original positions
        originalPosition = transform.position;
        otherObject.GetComponent<Swap>().originalPosition = otherObject.transform.position;
    }

    private void CheckOrder()
    {
        if (box.transform.position.y > bowl.transform.position.y && bowl.transform.position.y > cup.transform.position.y)
        {
            Debug.Log("Correct Order");
        }
    }
}

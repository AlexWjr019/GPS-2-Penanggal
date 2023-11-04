using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    public float moveSpeed = 3.0f;
    private Vector3 targetPosition;
    private bool puzzleCompleted = false;

    //public GameObject weddingSpawn;
    //public GameObject cursePaper;
    //public GameObject player;
    private bool canTp = true;

    private void Start()
    {
        originalPosition = transform.position;
        canTp = true;
    }

    //private void Update()
    //{
    //    if (cursePaper != null && canTp)
    //    {
    //        player.transform.position = new Vector3(-9.67962f, -2.085497f, -1.609497f);
    //        canTp = false;
    //    }
    //}

    private void OnMouseDown()
    {
        if (!puzzleCompleted && gameObject.CompareTag("Swap"))
        {
            screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
            offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
            isDragging = true;
        }
    }

    private void OnMouseDrag()
    {
        if (isDragging && !puzzleCompleted)
        {
            if (gameObject.CompareTag("Swap"))
            {
                Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
                Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
                transform.position = curPosition;
                weddingPuzzle = true;
            }

        }
    }

    private void OnMouseUp()
    {
        isDragging = false;
        weddingPuzzle = false;

        if (isColliding && collidingObject != null && gameObject.CompareTag("Swap") && collidingObject.CompareTag("Swap"))
        {
            if (collidingObject != null)
            {
                StartCoroutine(SmoothlyMoveToPosition(collidingObject, originalPosition));
            }

            if (this.gameObject != null)
            {
                StartCoroutine(SmoothlyMoveToPosition(this.gameObject, collidingObject.transform.position));
            }
        }
        else
        {
            // Snap the object back to its original position
            StartCoroutine(SmoothlyMoveToPosition(this.gameObject, originalPosition));
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

    private IEnumerator SmoothlyMoveToPosition(GameObject obj, Vector3 target)
    {
        if (obj == null)
        {
            Debug.LogError("Object is null. Aborting SmoothlyMoveToPosition coroutine.");
            yield break;
        }

        Transform objTransform = obj.transform;

        if (objTransform == null)
        {
            Debug.LogError("Object's transform component is null. Aborting SmoothlyMoveToPosition coroutine.");
            yield break;
        }

        float elapsedTime = 0f;
        Vector3 initialPosition = objTransform.position;
        Swap swapComponent = obj.GetComponent<Swap>();

        if (swapComponent != null)
        {
            Vector3 originalPosition = swapComponent.originalPosition;

            while (elapsedTime < 1f)
            {
                elapsedTime += Time.deltaTime * moveSpeed;
                FindObjectOfType<AudioManager>().PlaySFX("ObjectSwap");
                objTransform.position = Vector3.Lerp(initialPosition, target, elapsedTime);
                yield return null;
            }

            objTransform.position = target;

            // Update the original position of both objects after the swap
            swapComponent.originalPosition = target;

            // Update the original position of the colliding object if it has a Swap component
            if (collidingObject != null)
            {
                Swap collidingSwapComponent = collidingObject.GetComponent<Swap>();
                if (collidingSwapComponent != null)
                {
                    collidingSwapComponent.originalPosition = initialPosition;
                }
            }
        }
        else
        {
            Debug.LogError("Swap component not found on the object: " + obj.name);
        }
    }


    private void CheckOrder()
    {
        if (box.transform.position.y > bowl.transform.position.y && bowl.transform.position.y > cup.transform.position.y)
        {
            Debug.Log("Correct Order");
            puzzleCompleted = true;
            bowl.tag = "Unmovable";
            cup.tag = "Unmovable";
            box.tag = "Unmovable";
        }
    }
}
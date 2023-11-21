using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Swap : MonoBehaviour
{
    private Vector3 originalPosition;
    private Vector3 screenPoint;
    private Vector3 offset;
    private bool isDragging = false;
    private bool isColliding = false;
    private GameObject collidingObject;

    public GameObject bottle;
    public GameObject bowl;
    public GameObject flower;
    private List<GameObject> collidingObjects = new List<GameObject>();
    public static bool weddingPuzzle = false;
    public float moveSpeed = 3.0f;
    private Vector3 targetPosition;
    private bool puzzleCompleted = false;
    public bool startSwap = false, objective = false;
    private bool hasMouseUpExecuted = false;

    //public GameObject weddingSpawn;
    //public GameObject cursePaper;
    //public GameObject player;

    public GameObject cursePaper;

    private void Start()
    {
        originalPosition = transform.position;
    }

    private void Update()
    {
        Debug.Log("objective : " + objective);
        Debug.Log("start swap : "  + startSwap);

    }
    private void touchPuzzle()
    {
        ObjectiveManager2.objective = true;
    }

    private void OnMouseDown()
    {
        if (!puzzleCompleted && gameObject.CompareTag("Swap"))
        {
            screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
            offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
            isDragging = true;
        }

        if (startSwap == true && objective == false && hasMouseUpExecuted == false)
        {
            touchPuzzle();
            hasMouseUpExecuted = true;
            objective = true;
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

        if (!objective)
        {
            startSwap = true;
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
            CheckOrder();
        }
        else
        {
            // Snap the object back to its original position
            StartCoroutine(SmoothlyMoveToPosition(this.gameObject, originalPosition));
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Swap"))
        {
            isColliding = true;
            collidingObject = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Swap"))
        {
            isColliding = false;
            collidingObject = null;
        }
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
                //FindObjectOfType<AudioManager>().PlaySFX("ObjectSwap");
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
        Debug.Log("bottle " + bottle.transform.position);
        Debug.Log("bowl " + bowl.transform.position);
        Debug.Log("flower " + flower.transform.position);
        if (bottle.transform.position.x > bowl.transform.position.x && bowl.transform.position.x > flower.transform.position.x)
        {
            Debug.Log("Correct Order");
            puzzleCompleted = true;
            bowl.tag = "Unmovable";
            bottle.tag = "Unmovable";
            flower.tag = "Unmovable";
            StartCoroutine(SpawnCursepaper());
        }
    }

    IEnumerator SpawnCursepaper()
    {
        yield return new WaitForSeconds(1.0f);
        cursePaper.SetActive(true);
        ObjectiveManager2.objective = true;
    }
}
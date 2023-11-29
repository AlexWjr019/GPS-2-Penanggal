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
    public bool startSwap = true;
    public bool objective = false;

    private bool isBottle = false;
    private bool isBowl = false;
    private bool isFlower = false;

    //public GameObject weddingSpawn;
    //public GameObject cursePaper;
    //public GameObject player;
    private bool objectiveActive = true;
    private bool puzzleStart = false;
    public static bool objectiveTrue = false;

    public GameObject cursePaper;
    public Animator curtainAnimator;

    private void Start()
    {
        originalPosition = transform.position;
    }

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

    private void Update()
    {
        CheckOrder();
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
        //Debug.Log("bottle " + bottle.transform.position);
        //Debug.Log("bowl " + bowl.transform.position);
        //Debug.Log("flower " + flower.transform.position);
        //float tolerance = 0.01f;

        //if (bottle.transform.position.x > bowl.transform.position.x && bowl.transform.position.x > flower.transform.position.x
        //    /*&& bottle.transform.position.z < bowl.transform.position.z && bowl.transform.position.z > flower.transform.position.z*/)
        //{
        //    if (Mathf.Abs(bottle.transform.position.y - bowl.transform.position.y) < tolerance)
        //    {
        //        Debug.Log("Correct Order");
        //        puzzleCompleted = true;
        //        bowl.tag = "Unmovable";
        //        bottle.tag = "Unmovable";
        //        flower.tag = "Unmovable";
        //        curtainAnimator.SetBool("OpenCurtain", true);
        //        StartCoroutine(SpawnCursepaper());
        //    }

        //}

        if(PuzzleDetect1.isTriggered && PuzzleDetect2.isTriggered && PuzzleDetect3.isTriggered)
        {
            Debug.Log("Correct Order");
            puzzleCompleted = true;
            bowl.tag = "Unmovable";
            bottle.tag = "Unmovable";
            flower.tag = "Unmovable";
            curtainAnimator.SetBool("OpenCurtain", true);
            StartCoroutine(SpawnCursepaper());
        }
    }

    IEnumerator SpawnCursepaper()
    {
        yield return new WaitForSeconds(0.5f);
        cursePaper.SetActive(true);
        objectiveTrue = true;
    }
}
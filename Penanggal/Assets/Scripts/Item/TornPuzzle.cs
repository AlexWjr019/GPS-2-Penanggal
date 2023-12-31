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

    public float moveSpeed = 3.0f;

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
            CheckOrder();
        }
        //Debug.Log(isInteraction);

        CallBabyPenanggal();
    }

    private void OnMouseDown()
    {
        if(TornPuzzleControl.tornPuzzleActivated)
        {
            if (gameObject.CompareTag("Swap"))
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
        if(TornPuzzleControl.tornPuzzleActivated)
        {
            if (isDragging)
            {
                Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
                Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
                transform.position = curPosition;
            }
        }
    }

    private void OnMouseUp()
    {
        if (isRotating)
        {
            // Rotate the object 90 degrees clockwise when clicked
            transform.Rotate(Vector3.forward * 90f);
        }
        else if (isColliding && collidingObject != null && gameObject.CompareTag("Swap") && collidingObject.CompareTag("Swap"))
        {
            if (collidingObject != null)
            {
                //StartCoroutine(SmoothlyMoveToPosition(collidingObject, originalPosition));
                // Snap the dragged piece to the colliding piece's exact position
                StartCoroutine(SmoothlyMoveToPosition(this.gameObject, collidingObject.transform.position));
                // Snap the colliding piece to the original position of the dragged piece
                StartCoroutine(SmoothlyMoveToPosition(collidingObject, originalPosition));
            }

            //if (this.gameObject != null)
            //{
            //    //StartCoroutine(SmoothlyMoveToPosition(this.gameObject, collidingObject.transform.position));
            //}
        }
        else
        {
            // Snap the object back to its original position
            StartCoroutine(SmoothlyMoveToPosition(this.gameObject, originalPosition));
        }
        
        isDragging = false;
        isRotating = false;
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

        TornPuzzle tornComponent = obj.GetComponent<TornPuzzle>();

        if (tornComponent == null)
        {
            Debug.LogError("TornPuzzle component not found on the object: " + obj.name);
            yield break;
        }

        float elapsedTime = 0f;
        Vector3 initialPosition = objTransform.position;
        //TornPuzzle tornComponent = obj.GetComponent<TornPuzzle>();

        if (tornComponent != null)
        {
            Vector3 originalPosition = tornComponent.originalPosition;

            while (elapsedTime < 1f)
            {
                elapsedTime += Time.deltaTime * moveSpeed;
                //FindObjectOfType<AudioManager>().PlaySFX("DrawingPieceSwapSound");
                objTransform.position = Vector3.Lerp(initialPosition, target, elapsedTime);
                yield return null;
            }

            objTransform.position = target;

            // Update the original position of both objects after the swap
            tornComponent.originalPosition = target;

            // Update the original position of the colliding object if it has a Swap component
            if (collidingObject != null)
            {
                TornPuzzle collidingTornComponent = collidingObject.GetComponent<TornPuzzle>();
                if (collidingTornComponent != null)
                {
                    collidingTornComponent.originalPosition = initialPosition;
                }
            }

            CheckOrder();
        }
        else
        {
            Debug.LogError("Swap component not found on the object: " + obj.name);
        }
    }

    private void CheckOrder()
    {
        const float epsilon = 0.0001f;
        if ((Mathf.Abs((float)pic0.transform.rotation.z - 0.7207516f) < epsilon || Mathf.Abs((float)pic0.transform.rotation.z + 0.7207516f) < epsilon) &&
            (Mathf.Abs((float)pic1.transform.rotation.z - 0.7207516f) < epsilon || Mathf.Abs((float)pic1.transform.rotation.z + 0.7207516f) < epsilon) &&
            (Mathf.Abs((float)pic2.transform.rotation.z - 0.7207516f) < epsilon || Mathf.Abs((float)pic2.transform.rotation.z + 0.7207516f) < epsilon) &&
            (Mathf.Abs((float)pic3.transform.rotation.z - 0.7207516f) < epsilon || Mathf.Abs((float)pic3.transform.rotation.z + 0.7207516f) < epsilon) &&
            (Mathf.Abs((float)pic4.transform.rotation.z - 0.7207516f) < epsilon || Mathf.Abs((float)pic4.transform.rotation.z + 0.7207516f) < epsilon) &&
            (Mathf.Abs((float)pic5.transform.rotation.z - 0.7207516f) < epsilon || Mathf.Abs((float)pic5.transform.rotation.z + 0.7207516f) < epsilon) &&
            (Mathf.Abs((float)pic6.transform.rotation.z - 0.7207516f) < epsilon || Mathf.Abs((float)pic6.transform.rotation.z + 0.7207516f) < epsilon) &&
            (Mathf.Abs((float)pic7.transform.rotation.z - 0.7207516f) < epsilon || Mathf.Abs((float)pic7.transform.rotation.z + 0.7207516f) < epsilon) &&
            (Mathf.Abs((float)pic8.transform.rotation.z - 0.7207516f) < epsilon || Mathf.Abs((float)pic8.transform.rotation.z + 0.7207516f) < epsilon))
        {
            if (pic0.transform.position.x > pic1.transform.position.x && pic1.transform.position.x > pic2.transform.position.x &&
                pic3.transform.position.x > pic4.transform.position.x && pic4.transform.position.x > pic5.transform.position.x &&
                pic6.transform.position.x > pic7.transform.position.x && pic7.transform.position.x > pic8.transform.position.x &&
                pic0.transform.position.y < pic3.transform.position.y && pic3.transform.position.y < pic6.transform.position.y &&
                pic1.transform.position.y < pic4.transform.position.y && pic4.transform.position.y < pic7.transform.position.y &&
                pic2.transform.position.y < pic5.transform.position.y && pic5.transform.position.y < pic8.transform.position.y)
            {
                Debug.Log("Correct Order");
                isDragging = false;
                isRotating = false;
                callBaby = false;
                TornPuzzleControl.isTorn = true;
                CreateWholePicture(); // Call the method to create the whole picture
                BabySpawner.Instance.spawnBaby = false;
                BabySpawner.Instance.StopCoroutine(BabySpawner.Instance.SpawnBaby());
            }
        }
            

        //Debug.Log("pic 0 x " + pic0.transform.position.x);
        //Debug.Log("pic 1 x " + pic1.transform.position.x);
        //Debug.Log("pic 2 x " + pic2.transform.position.x);
        //Debug.Log("pic 3 x " + pic3.transform.position.x);
        //Debug.Log("pic 4 x " + pic4.transform.position.x);
        //Debug.Log("pic 5 x " + pic5.transform.position.x);
        //Debug.Log("pic 6 x " + pic6.transform.position.x);
        //Debug.Log("pic 7 x " + pic7.transform.position.x);
        //Debug.Log("pic 8 x " + pic8.transform.position.x);

        //Debug.Log("pic 0 y " + pic0.transform.position.y);
        //Debug.Log("pic 1 y " + pic1.transform.position.y);
        //Debug.Log("pic 2 y " + pic2.transform.position.y);
        //Debug.Log("pic 3 y " + pic3.transform.position.y);
        //Debug.Log("pic 4 y " + pic4.transform.position.y);
        //Debug.Log("pic 5 y " + pic5.transform.position.y);
        //Debug.Log("pic 6 y " + pic6.transform.position.y);
        //Debug.Log("pic 7 y " + pic7.transform.position.y);
        //Debug.Log("pic 8 y" + pic8.transform.position.y);

        //Debug.Log("pic 0 z " + pic0.transform.rotation.z);
        //Debug.Log("pic 1 z " + pic1.transform.rotation.z);
        //Debug.Log("pic 2 z " + pic2.transform.rotation.z);
        //Debug.Log("pic 3 z " + pic3.transform.rotation.z);
        //Debug.Log("pic 4 z " + pic4.transform.rotation.z);
        //Debug.Log("pic 5 z " + pic5.transform.rotation.z);
        //Debug.Log("pic 6 z " + pic6.transform.rotation.z);
        //Debug.Log("pic 7 z " + pic7.transform.rotation.z);
        //Debug.Log("pic 8 z " + pic8.transform.rotation.z);

    }

    private void CreateWholePicture()
    {
        // Create the whole picture by enabling it
        wholePicture.SetActive(true);
        getPinPaper = true;
        picPuzzle.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TornPuzzleControl : MonoBehaviour
{
    public static bool isTorn;
    private bool isTornPuzzle;
    public GameObject tornPuzzle;
    private float raycastDistance = 5f;
    public GameObject playerCamera;
    public static bool tornPuzzleActivated;
    //public InteracionItem interacionItem;
    public GameObject wholePic;
    private int itemsMask;
    private bool puzzleCompleted;
    private bool objectiveActive = true;
    public GameObject pauseButton;

    private void Awake()
    {
        itemsMask = LayerMask.NameToLayer("Items");
    }

    // Start is called before the first frame update
    void Start()
    {
        isTornPuzzle = false;
        tornPuzzleActivated = false;
        isTorn = false;
        puzzleCompleted = false;
        //interacionItem = GetComponent<InteracionItem>();
        //interacionItem.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isTornPuzzle && !puzzleCompleted)
        {
            CheckPuzzleActivated();
            isTornPuzzle = false;
        }

        if (isTorn)
        {
            TornPuzzleCompleted();
            isTorn = false;
        }
    }

    private void CheckPuzzleActivated()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(touch.position);

                if (Physics.Raycast(ray, out hit, raycastDistance))
                {
                    if (hit.collider.gameObject.CompareTag("TornDrawing"))
                    {
                        tornPuzzle.SetActive(true);
                        pauseButton.SetActive(false);
                        isTornPuzzle = true;
                        playerCamera.SetActive(false);
                        tornPuzzleActivated = true;
                        Cursor.lockState = CursorLockMode.None;
                        Cursor.visible = true;
                        if (objectiveActive)
                        {
                            ObjectiveManager.objective = true;
                            objectiveActive = false;
                        }

                    }
                }
            }
        }
        else if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, raycastDistance))
            {
                if (hit.collider.gameObject.CompareTag("TornDrawing"))
                {
                    tornPuzzle.SetActive(true);
                    pauseButton.SetActive(false);
                    isTornPuzzle = true;
                    playerCamera.SetActive(false);
                    tornPuzzleActivated = true;
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    if (objectiveActive)
                    {
                        ObjectiveManager.objective = true;
                        objectiveActive = false;
                    }
                }
            }
        }
    }

    public void TornPuzzleCompleted()
    {
        puzzleCompleted = true;
        tornPuzzle.SetActive(false);
        isTornPuzzle = false;
        playerCamera.SetActive(true);
        tornPuzzleActivated = false;
        //interacionItem.enabled = true;
        wholePic.layer = itemsMask;
        ObjectiveManager.objective = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void TornPuzzleReturn()
    {
        tornPuzzle.SetActive(false);
        isTornPuzzle = false;
        playerCamera.SetActive(true);
        tornPuzzleActivated = false;
        pauseButton.SetActive(true);
        //interacionItem.enabled = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}

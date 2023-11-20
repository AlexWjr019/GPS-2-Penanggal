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
                        isTornPuzzle = true;
                        playerCamera.SetActive(false);
                        tornPuzzleActivated = true;
                        ObjectiveManager.objective = true;
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
    }

    public void TornPuzzleReturn()
    {
        tornPuzzle.SetActive(false);
        isTornPuzzle = false;
        playerCamera.SetActive(true);
        tornPuzzleActivated = false;
        //interacionItem.enabled = false;
    }
}

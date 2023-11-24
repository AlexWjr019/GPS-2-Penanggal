using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class PuzzleActive : MonoBehaviour
{
    public static bool isAlter;
    private bool isAlterPuzzle;
    public GameObject alterCamera;
    private float raycastDistance = 5f;
    public GameObject playerCamera;
    public static bool alterPuzzleActivated;
    private bool puzzleCompleted;
    private bool objectiveActive = true;
    public GameObject button;

    void Update()
    {
        if (!isAlterPuzzle && !puzzleCompleted)
        {
            CheckPuzzleActivated();
            isAlterPuzzle = false;
        }

        if (isAlter)
        {
            //TornPuzzleCompleted();
            isAlter = false;
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
                    if (hit.collider.gameObject.CompareTag("AlterPuzzle"))
                    {
                        alterCamera.SetActive(true);
                        isAlterPuzzle = true;
                        playerCamera.SetActive(false);
                        button.SetActive(true);
                        alterPuzzleActivated = true;
                        if (objectiveActive)
                        {
                            ObjectiveManager2.objective = true;
                            objectiveActive = false;
                        }

                    }
                }
            }
        }
    }

    public void AlterPuzzleReturn()
    {
        alterCamera.SetActive(false);
        isAlterPuzzle = false;
        playerCamera.SetActive(true);
        button.SetActive(false);
        alterPuzzleActivated = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hide : MonoBehaviour
{
    public static bool isHide;

    public GameObject hideCamera;
    public GameObject player;
    public GameObject playerCapsule;
    public LayerMask interactableLayer;
    private GameObject currentInteractable;
    private float raycastDistance = 3f;
    public GameObject pointToCupBoard;

    public Animation cupBoardDoorAnima, cupboardDoorAnima2;

    public bool livingCupBoard, hallCupBoard, masterCupboard, kitchenCupBoard;

    private void Start()
    {
        isHide = false;
        hideCamera.SetActive(false);
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                if (isHide == false)
                {
                    Ray ray = Camera.main.ScreenPointToRay(touch.position);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit, raycastDistance, interactableLayer))
                    {
                        currentInteractable = hit.collider.gameObject;

                        if (currentInteractable.CompareTag("Hide"))
                        {
                            // Disable Player Camera
                            player.SetActive(false);
                            isHide = true;

                            Tutorial tutorial = FindObjectOfType<Tutorial>();
                            tutorial.hideCupboardText.gameObject.SetActive(false);
                            Destroy(pointToCupBoard);

                            if (livingCupBoard && currentInteractable.gameObject.name == "Livingroom_Cupboard")
                            {
                                hideCamera.SetActive(true);
                                cupBoardDoorAnima.Play("CloseCupboard");
                                cupboardDoorAnima2.Play("CloseCupboard2");
                            }
                            //else if (kitchenCupBoard && currentInteractable.gameObject.name == "Kitchen_Cupboard")
                            //{
                            //    //kitchenHideCamera.SetActive(true);
                            //    cupBoardDoorAnima.Play("CloseCupboard3");
                            //    cupboardDoorAnima2.Play("CloseCupboard4");
                            //}
                            //else if (masterCupboard && currentInteractable.gameObject.name == "Masterroom_Cupboard")
                            //{
                            //    masterHideCamera.SetActive(true);
                            //    cupBoardDoorAnima.Play("CloseCupboard5");
                            //    cupboardDoorAnima2.Play("CloseCupboard6");
                            //}
                            //else if (hallCupBoard && currentInteractable.gameObject.name == "Hall_Cupboard")
                            //{
                            //    hallHideCamera.SetActive(true);
                            //    cupBoardDoorAnima.Play("CloseCupboard7");
                            //    cupboardDoorAnima2.Play("CloseCupboard8");
                            //}
                        }
                    }
                }
                else
                {
                    if (livingCupBoard)
                    {
                        //int cameraIndex = 0; // Replace with the correct index based on your logic
                        Ray ray = hideCamera.GetComponent<Camera>().ScreenPointToRay(touch.position);
                        RaycastHit hit;
                        if (Physics.Raycast(ray, out hit, raycastDistance, interactableLayer))
                        {
                            currentInteractable = hit.collider.gameObject;

                            if (currentInteractable.CompareTag("Hide"))
                            {
                                //Player Camera
                                player.SetActive(true);
                                isHide = false;
                                if (livingCupBoard && currentInteractable.gameObject.name == "Livingroom_Cupboard")
                                {
                                    hideCamera.SetActive(false);
                                    cupBoardDoorAnima.Play("OpenCupboard");
                                    cupboardDoorAnima2.Play("OpenCupboard2");
                                }

                                playerCapsule.transform.rotation = Quaternion.Euler(transform.rotation.x, -180f, transform.rotation.z);
                            }
                        }
                    }
                    //else if (masterCupboard)
                    //{
                    //    //int cameraIndex = 0; // Replace with the correct index based on your logic
                    //    Ray ray = masterHideCamera.GetComponent<Camera>().ScreenPointToRay(touch.position);
                    //    RaycastHit hit;
                    //    if (Physics.Raycast(ray, out hit, raycastDistance, interactableLayer))
                    //    {
                    //        currentInteractable = hit.collider.gameObject;

                    //        if (currentInteractable.CompareTag("Hide"))
                    //        {
                    //            //Player Camera
                    //            player.SetActive(true);
                    //            isHide = false;
                    //            if (masterCupboard && currentInteractable.gameObject.name == "Masterroom_Cupboard")
                    //            {
                    //                masterHideCamera.SetActive(false);
                    //                cupBoardDoorAnima.Play("OpenCupboard5");
                    //                cupboardDoorAnima2.Play("OpenCupboard6");
                    //            }
                    //            playerCapsule.transform.rotation = Quaternion.Euler(transform.rotation.x, -180f, transform.rotation.z);
                    //        }
                    //    }
                    //}
                    //else if (hallCupBoard)
                    //{
                    //    //int cameraIndex = 0; // Replace with the correct index based on your logic
                    //    Ray ray = hallHideCamera.GetComponent<Camera>().ScreenPointToRay(touch.position);
                    //    RaycastHit hit;
                    //    if (Physics.Raycast(ray, out hit, raycastDistance, interactableLayer))
                    //    {
                    //        currentInteractable = hit.collider.gameObject;

                    //        if (currentInteractable.CompareTag("Hide"))
                    //        {
                    //            //Player Camera
                    //            player.SetActive(true);
                    //            isHide = false;

                    //            if (hallCupBoard && currentInteractable.gameObject.name == "Hall_Cupboard")
                    //            {
                    //                hallHideCamera.SetActive(false);
                    //                cupBoardDoorAnima.Play("OpenCupboard7");
                    //                cupboardDoorAnima2.Play("OpenCupboard8");
                    //            }

                    //            playerCapsule.transform.rotation = Quaternion.Euler(transform.rotation.x, -180f, transform.rotation.z);
                    //        }
                    //    }
                    //}
                    //else if (kitchenCupBoard)
                    //{
                    //    //int cameraIndex = 0; // Replace with the correct index based on your logic
                    //    //Ray ray = kitchenHideCamera.GetComponent<Camera>().ScreenPointToRay(touch.position);
                    //    //RaycastHit hit;
                    //    //if (Physics.Raycast(ray, out hit, raycastDistance, interactableLayer))
                    //    {
                    //        //currentInteractable = hit.collider.gameObject;

                    //        if (currentInteractable.CompareTag("Hide"))
                    //        {
                    //            //Player Camera
                    //            player.SetActive(true);
                    //            isHide = false;
                    //            if (kitchenCupBoard && currentInteractable.gameObject.name == "Kitchen_Cupboard")
                    //            {
                    //                //kitchenHideCamera.SetActive(false);
                    //                cupBoardDoorAnima.Play("OpenCupboard3");
                    //                cupboardDoorAnima2.Play("OpenCupboard4");
                    //            }

                    //            playerCapsule.transform.rotation = Quaternion.Euler(transform.rotation.x, -180f, transform.rotation.z);
                    //        }
                    //    }
                    //}
                }
            }
        }
    }

}

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

                            if (currentInteractable.gameObject.name == "Livingroom_Cupboard")
                            {
                                hideCamera.SetActive(true);
                                cupBoardDoorAnima.Play("CloseCupboardDoor");
                                cupboardDoorAnima2.Play("CloseCupboardDoor2");
                            }
                        }
                    }
                }
                else
                {
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
                            if (currentInteractable.gameObject.name == "Livingroom_Cupboard")
                            {
                                hideCamera.SetActive(false);
                                cupBoardDoorAnima.Play("OpenCupboardDoor");
                                cupboardDoorAnima2.Play("OpenCupboardDoor2");
                            }

                            playerCapsule.transform.rotation = Quaternion.Euler(transform.rotation.x, -180f, transform.rotation.z);
                        }
                    }
                }
            }
        }
    }

}

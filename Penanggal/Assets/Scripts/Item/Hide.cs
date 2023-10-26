using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hide : MonoBehaviour
{
    public bool isHide;
    private bool isTransitioning;

    public LayerMask interactableLayer;
    private GameObject currentInteractable;
    private float raycastDistance = 3f;


    public Animation cupBoardDoorAnima, cupboardDoorAnima2, hideInLivingCupboard, moveOutLivingCupboard, 
        hideInHallwayCupboard, moveOutHallwayCupboard, hallwayDoorAnima, hallwayDoorAnima2, 
        bedroomDoorAnima, bedroomDoorAnima2, hideInBedroomCupboard, moveOutBedroomCupboard,
        kitchenDoorAnima, kitchenDoorAnima2, hideInKitchenCupboard, moveOutKitchenCupboard;
    public bool livingCupboard, hallwayCupboard, bedroomCupboard, kitchenCupboard;

    private void Start()
    {
        isHide = false;
        isTransitioning = false;
    }

    private void Update()
    {
        if (!isTransitioning && (Input.touchCount > 0 || Input.GetMouseButtonDown(0)) && FirstPersonController.canHide) // Check for touch or left mouse button click
        {
            Vector2 inputPosition;

            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                inputPosition = touch.position;
            }
            else
            {
                inputPosition = Input.mousePosition;
            }

            if (isHide == false)
            {
                Ray ray = Camera.main.ScreenPointToRay(inputPosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, raycastDistance, interactableLayer))
                {
                    currentInteractable = hit.collider.gameObject;

                    if (currentInteractable.CompareTag("Hide"))
                    {
                        // Disable Player Camera
                        //player.SetActive(false);
                        isHide = true;
                        isTransitioning = true;

                        //Tutorial tutorial = FindObjectOfType<Tutorial>();
                        //tutorial.hideCupboardText.gameObject.SetActive(false);
                        //Destroy(pointToCupBoard);

                        if (currentInteractable.gameObject.name == "Livingroom_Cupboard" || currentInteractable.gameObject.name == "Hallway_Cupboard" || currentInteractable.gameObject.name == "Bedroom_Cupboard" || currentInteractable.gameObject.name == "Kitchen_Cupboard")
                        {
                            if(currentInteractable.gameObject.name == "Livingroom_Cupboard")
                            {
                                livingCupboard = true;
                                hallwayCupboard = false;
                                bedroomCupboard = false;
                                kitchenCupboard = false;
                            }
                            else if (currentInteractable.gameObject.name == "Hallway_Cupboard")
                            {
                                livingCupboard = false;
                                hallwayCupboard = true;
                                bedroomCupboard = false;
                                kitchenCupboard = false;
                            }
                            else if (currentInteractable.gameObject.name == "Bedroom_Cupboard")
                            {
                                livingCupboard = false;
                                hallwayCupboard = false;
                                bedroomCupboard = true;
                                kitchenCupboard = false;
                            }
                            else if (currentInteractable.gameObject.name == "Kitchen_Cupboard")
                            {
                                livingCupboard = false;
                                hallwayCupboard = false;
                                bedroomCupboard = false;
                                kitchenCupboard = true;
                            }

                        }
                        StartCoroutine(PlayHideInCupboardThenCloseDoors());
                    }
                }
            }
            else
            {
                Ray ray = Camera.main.ScreenPointToRay(inputPosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, raycastDistance, interactableLayer))
                {
                    currentInteractable = hit.collider.gameObject;

                    if (currentInteractable.CompareTag("Hide"))
                    {

                        isHide = false;
                        isTransitioning = true;

                        if (currentInteractable.gameObject.name == "LivingDoor1" || currentInteractable.gameObject.name == "LivingDoor2" || currentInteractable.gameObject.name == "HallwayDoor1" || currentInteractable.gameObject.name == "HallwayDoor2" || currentInteractable.gameObject.name == "BedroomDoor1" || currentInteractable.gameObject.name == "BedroomDoor2" || currentInteractable.gameObject.name == "KitchenDoor1" || currentInteractable.gameObject.name == "KitchenDoor2")
                        {
                            if (currentInteractable.gameObject.name == "LivingDoor1" || currentInteractable.gameObject.name == "LivingDoor2")
                            {
                                livingCupboard = true;
                                hallwayCupboard = false;
                                bedroomCupboard = false;
                                kitchenCupboard = false;
                            }
                            else if (currentInteractable.gameObject.name == "HallwayDoor1" || currentInteractable.gameObject.name == "HallwayDoor2")
                            {
                                livingCupboard = false;
                                hallwayCupboard = true;
                                bedroomCupboard = false;
                                kitchenCupboard = false;
                            }
                            else if (currentInteractable.gameObject.name == "BedroomDoor1" || currentInteractable.gameObject.name == "BedroomDoor2")
                            {
                                livingCupboard = false;
                                hallwayCupboard = false;
                                bedroomCupboard = true;
                                kitchenCupboard = false;
                            }
                            else if (currentInteractable.gameObject.name == "KitchenDoor1" || currentInteractable.gameObject.name == "KitchenDoor2")
                            {
                                livingCupboard = false;
                                hallwayCupboard = false;
                                bedroomCupboard = false;
                                kitchenCupboard = true;
                            }
                            StartCoroutine(PlayMoveOutCupboard());
                        }

                    }
                }
            }
        }
    }


    IEnumerator PlayHideInCupboardThenCloseDoors()
    {
        if (livingCupboard)
        {
            hideInLivingCupboard.Play("HideInLivingCupboard"); // Play the first animation

            // Wait for 2 seconds
            yield return new WaitForSeconds(2.0f);

            // Now, play the other two animations
            cupBoardDoorAnima.Play("CloseCupboardDoor");
            cupboardDoorAnima2.Play("CloseCupboardDoor2");
        }

        if(hallwayCupboard)
        {
            hideInHallwayCupboard.Play("HideInHallwayCupboard"); // Play the first animation

            // Wait for 2 seconds
            yield return new WaitForSeconds(2.0f);

            // Now, play the other two animations
            hallwayDoorAnima.Play("HallwayCloseDoor1");
            hallwayDoorAnima2.Play("HallwayCloseDoor2");
        }

        if (bedroomCupboard)
        {
            hideInBedroomCupboard.Play("HideInBedroomCupboard"); // Play the first animation

            // Wait for 2 seconds
            yield return new WaitForSeconds(2.0f);

            // Now, play the other two animations
            bedroomDoorAnima.Play("BedroomCloseDoor1");
            bedroomDoorAnima2.Play("BedroomCloseDoor2");
        }

        if (kitchenCupboard)
        {
            hideInKitchenCupboard.Play("HideInKitchenCupboard"); // Play the first animation

            // Wait for 2 seconds
            yield return new WaitForSeconds(2.0f);

            // Now, play the other two animations
            kitchenDoorAnima.Play("KitchenCloseDoor1");
            kitchenDoorAnima2.Play("KitchenCloseDoor2");
        }

        isTransitioning = false;
    }

    IEnumerator PlayMoveOutCupboard()
    {
        if (livingCupboard)
        {
            cupBoardDoorAnima.Play("OpenCupboardDoor");
            cupboardDoorAnima2.Play("OpenCupboardDoor2");

            yield return new WaitForSeconds(1.0f);

            moveOutLivingCupboard.Play("MoveOutLivingCupboard");
        }

        if(hallwayCupboard)
        {
            hallwayDoorAnima.Play("HallwayOpenDoor1");
            hallwayDoorAnima2.Play("HallwayOpenDoor2");

            yield return new WaitForSeconds(1.0f);

            moveOutHallwayCupboard.Play("MoveOutHallwayCupboard");
        }

        if (bedroomCupboard)
        {
            bedroomDoorAnima.Play("BedroomOpenDoor1");
            bedroomDoorAnima2.Play("BedroomOpenDoor2");

            yield return new WaitForSeconds(1.0f);

            moveOutBedroomCupboard.Play("MoveOutBedroomCupboard");
        }

        if (kitchenCupboard)
        {
            kitchenDoorAnima.Play("KitchenOpenDoor1");
            kitchenDoorAnima2.Play("KitchenOpenDoor2");

            yield return new WaitForSeconds(1.0f);

            moveOutKitchenCupboard.Play("MoveOutKitchenCupboard");
        }

        isTransitioning = false;

    }

}

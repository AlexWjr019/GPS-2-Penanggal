using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OpenDoor : MonoBehaviour
{
    public Animation doorAnimations;
    private bool doorIsOpen;

    private void OnMouseDown()
    {
        // Create a ray from the mouse position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Check if the ray hits any GameObject
        if (Physics.Raycast(ray, out hit))
        {
            GameObject clickedObject = hit.collider.gameObject;

            // Check if the clicked GameObject is the door
            if (clickedObject.CompareTag("Door"))
            {
                if (Key.getKey && !doorIsOpen)
                {
                    doorIsOpen = true;
                    doorAnimations.Play("OpenDoor");
                }
                else if(doorIsOpen)
                {
                    doorIsOpen = false;
                    doorAnimations.Play("CloseDoor");
                }
            }
        }

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Check if the GameObject that was clicked is the door
        if (eventData.pointerCurrentRaycast.gameObject.CompareTag("Door"))
        {
            if (Key.getKey && !doorIsOpen)
            {
                doorIsOpen = true;
                doorAnimations.Play("OpenDoor");
            }
            else if (doorIsOpen)
            {
                doorIsOpen = false;
                doorAnimations.Play("CloseDoor");
            }
        }
    }
}

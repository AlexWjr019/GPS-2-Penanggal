using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionSystem : MonoBehaviour
{
    public LayerMask interactableLayer;
    public GameObject interactionUI;
    private GameObject currentInteractable;

    private float raycastDistance = 3f;

    private void Update()
    {
        Vector3 cameraPosition = Camera.main.transform.position;
        Vector3 cameraForward = Camera.main.transform.forward;
        Ray ray = new Ray(cameraPosition, cameraForward);

        Debug.DrawRay(ray.origin, ray.direction * raycastDistance, Color.blue, 1f);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, raycastDistance, interactableLayer))
        {
            GameObject hitObject = hit.collider.gameObject;

            if (hitObject != currentInteractable)
            {
                HideInteractionUI();

                ShowInteractionUI(hitObject);

                currentInteractable = hitObject;
            }
            else
            {
                // Check for button click in the Update method
                CheckForButtonClick();
            }
        }
        else
        {
            HideInteractionUI();
            currentInteractable = null;
        }
    }

    private void ShowInteractionUI(GameObject interactableObject)
    {
        if (interactionUI != null)
        {
            interactionUI.SetActive(true);
        }
    }

    private void HideInteractionUI()
    {
        if (interactionUI != null)
        {
            interactionUI.SetActive(false);
        }
    }

    private void CheckForButtonClick()
    {
        if (Input.GetButtonDown("Button1"))
        {
            // Call the interaction function when the button is clicked
            InteractWithCurrentObject();
        }
    }

    public void InteractWithCurrentObject()
    {
        if (currentInteractable != null)
        {
            // Implement your interaction logic here
            currentInteractable.SetActive(false);

            // Hide the interaction UI
            HideInteractionUI();
        }
    }
}


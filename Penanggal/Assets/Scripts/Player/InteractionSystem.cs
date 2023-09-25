using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionSystem : MonoBehaviour
{
    public LayerMask interactableLayer;
    public GameObject interactionUI;
    private GameObject currentInteractable;

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);


            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;

                Debug.DrawRay(ray.origin, ray.direction * 100f, Color.blue, 1f); // Color.blue 表示线的颜色，1f 表示线的持续时间

                if (Physics.Raycast(ray, out hit, Mathf.Infinity, interactableLayer))
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
                        // If the same object or UI is touched again, perform the interaction (make it disappear).
                        InteractWithObject(hitObject);

                        // Hide the interaction UI
                        HideInteractionUI();
                    }
                }
                else
                {
                    // If the touch didn't hit any interactable object, hide the interaction UI.
                    HideInteractionUI();
                    currentInteractable = null;
                }
            }
        }
    }

    private void ShowInteractionUI(GameObject interactableObject)
    {
        if (interactionUI != null)
        {
            interactionUI.SetActive(true);
            // You can set the position of the UI element based on the interactableObject's position here.
            // For example: interactionUI.transform.position = interactableObject.transform.position + Vector3.up * 2.0f;
        }
    }

    private void HideInteractionUI()
    {
        if (interactionUI != null)
        {
            interactionUI.SetActive(false);
        }
    }

    private void InteractWithObject(GameObject interactableObject)
    {
        // You can implement your interaction logic here, such as making the object disappear.
        interactableObject.SetActive(false);
    }
}

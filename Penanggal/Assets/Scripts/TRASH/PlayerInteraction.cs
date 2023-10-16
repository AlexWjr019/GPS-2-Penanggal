using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Interaction Settings")]
    public float maxDistance = 10.0f;
    public LayerMask interactableLayers;

    [Header("UI: adapt this to fit your game")]
    public Button interactButton;

    private Interactables currentInteractable;

    // Update is called once per frame
    void Update()
    {
        if(Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, maxDistance, interactableLayers))
        {
            currentInteractable = hit.collider.GetComponent<Interactables>();
            Debug.Log("item!");
        }
        else
        {
            currentInteractable = null;
            Debug.Log("no item");
        }

        interactButton.interactable = currentInteractable != null;
    }

    public void Interact()
    {
        if(currentInteractable)
        {
            currentInteractable.OnInteraction();
        }
    }
}

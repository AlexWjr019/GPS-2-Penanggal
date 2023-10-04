using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InteractionSystem : MonoBehaviour
{
    public LayerMask interactableLayer;
    public GameObject interactionUI;
    private GameObject currentInteractable;
    public InventoryItems inventoryItemPrefab;

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

                currentInteractable = hitObject;
            }
            else
            {
                CheckForButtonClick();
            }
        }
        else
        {
            currentInteractable = null;
        }
    }

    private void CheckForButtonClick()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, raycastDistance, interactableLayer))
                {
                    currentInteractable = hit.collider.gameObject;

                    InteractWithCurrentObject();
                }
            }
        }
    }


    public void InteractWithCurrentObject()
    {
        if (currentInteractable != null)
        {
            InventorySlot emptySlot = InventoryManager.Instance.GetEmptySlot();
            if (emptySlot != null)
            {
                InventoryItems newInventoryItem = Instantiate(inventoryItemPrefab, emptySlot.transform);
                newInventoryItem.transform.localPosition = Vector3.zero;
                newInventoryItem.transform.localScale = Vector3.one;
                newInventoryItem.name = "InventoryItem";
                newInventoryItem.gameObject.SetActive(true);

                currentInteractable.SetActive(false);
            }
            else
            {
                Debug.LogError("inventoryItemPrefab is not assigned!");
                Debug.LogWarning("没有可用的 InventorySlot 来放置物品！");
            }
        }
    }
}

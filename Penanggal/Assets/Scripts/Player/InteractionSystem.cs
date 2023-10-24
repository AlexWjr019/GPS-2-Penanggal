using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionSystem : MonoBehaviour
{
    public LayerMask interactableLayer;
    public GameObject interactionUI;
    public InventoryItems inventoryItemPrefab;

    private GameObject currentInteractable;
    private Vector3 lastInteractedItemPosition;
    private float raycastDistance = 3f;
    private float sphereRadius = 0.5f;

    private void Update()
    {
        Vector3 cameraPosition = Camera.main.transform.position;
        Vector3 cameraForward = Camera.main.transform.forward;
        Ray ray = new Ray(cameraPosition, cameraForward);

        //Debug.DrawRay(ray.origin, ray.direction * raycastDistance, Color.blue, 1f);

        RaycastHit hit;

        if (Physics.SphereCast(ray, sphereRadius, out hit, raycastDistance, interactableLayer))
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

                if (Physics.SphereCast(ray, sphereRadius, out hit, raycastDistance, interactableLayer))
                {
                    currentInteractable = hit.collider.gameObject;
                    InteractWithCurrentObject();
                }
            }
        }
    }

    public Vector3 LastInteractedItemPosition()
    {
        return lastInteractedItemPosition;
    }

    public void InteractWithCurrentObject()
    {
        if (currentInteractable != null)
        {
            IInteractableItem interactableItem = currentInteractable.GetComponent<IInteractableItem>();
            if (interactableItem != null)
            {
                string itemName = interactableItem.GetName();
                Sprite itemSprite = interactableItem.GetSprite();
                GameObject itemPrefab3D = interactableItem.GetPrefab3D();

                if (InventoryManager.Instance.IsInventoryFull())
                {
                    InventorySlot slotToReplace = InventoryManager.Instance.GetSlotToReplace();
                    Vector3 dropPosition = LastInteractedItemPosition();
                    ReplaceItem(slotToReplace, itemName, itemSprite, itemPrefab3D, dropPosition);
                }
                else
                {
                    InventorySlot emptySlot = InventoryManager.Instance.GetEmptySlot();
                    AddItemToSlot(emptySlot, itemName, itemSprite, itemPrefab3D);
                    currentInteractable.SetActive(false);
                }
            }
            else
            {
                Debug.LogWarning("The interactable object does not implement IInteractableItem!");
            }
        }
    }
    private void AddItemToSlot(InventorySlot slot, string itemName, Sprite itemSprite, GameObject itemPrefab3D)
    {
        Debug.Log("Adding item: " + itemName + " with prefab: " + itemPrefab3D);

        lastInteractedItemPosition = currentInteractable.transform.position;
        InventoryItems newInventoryItem = Instantiate(inventoryItemPrefab, slot.transform);
        newInventoryItem.transform.localPosition = Vector3.zero;
        newInventoryItem.transform.localScale = Vector3.one;
        newInventoryItem.name = itemName;
        newInventoryItem.gameObject.SetActive(true);
        newInventoryItem.Configure(itemName, itemSprite, itemPrefab3D);
        currentInteractable.SetActive(false);
    }
    public void ReplaceItem(InventorySlot selectedSlot, string newItemName, Sprite newItemSprite, GameObject newItemPrefab, Vector3 dropPosition)
    {
        if (selectedSlot != null && !selectedSlot.IsEmpty())
        {
            InventoryItems currentItem = selectedSlot.GetCurrentItem();

            currentItem.itemPrefab3D.transform.position = currentInteractable.transform.position;
            currentItem.itemPrefab3D.SetActive(true);

            currentItem.Configure(newItemName, newItemSprite, newItemPrefab);

            currentInteractable.SetActive(false);
        }
        else
        {
            Debug.LogError("No slot is selected or slot is empty");
        }
    }
    private void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;

        Vector3 start = Camera.main.transform.position;
        Vector3 end = start + Camera.main.transform.forward * raycastDistance;
        Vector3 currentPos = start;

        while (Vector3.Distance(currentPos, start) < raycastDistance)
        {
            Gizmos.DrawWireSphere(currentPos, sphereRadius);
            currentPos += Camera.main.transform.forward * sphereRadius * 2;
        }
    }


}

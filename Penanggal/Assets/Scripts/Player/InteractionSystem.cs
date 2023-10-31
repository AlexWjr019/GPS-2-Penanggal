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
    private Vector2 touchStartPosition;
    private float raycastDistance = 3f;
    private float sphereRadius = 0.5f;

    private float touchStartTime;
    private float clickDurationThreshold = 0.1f;

    public void Start()
    {

    }
    private void Update()
    {
        Vector3 cameraPosition = Camera.main.transform.position;
        Vector3 cameraForward = Camera.main.transform.forward;
        Ray ray = new Ray(cameraPosition, cameraForward);
        RaycastHit hit;

        if (Physics.SphereCast(ray, sphereRadius, out hit, raycastDistance))
        {
            if (hit.collider.CompareTag("Interactable"))
            {
                Debug.Log("Helloworld");
                TestingPlayAnimation cabinetAnimator = hit.collider.gameObject.GetComponent<TestingPlayAnimation>();
                if (Input.touchCount > 0)
                {
                    Touch touch = Input.GetTouch(0);
                    switch (touch.phase)
                    {
                        case TouchPhase.Began:
                            touchStartPosition = touch.position;
                            touchStartTime = Time.time;
                            break;
                        case TouchPhase.Ended:
                            float touchDuration = Time.time - touchStartTime;
                            if (Vector2.Distance(touch.position, touchStartPosition) < 30f && touchDuration <= clickDurationThreshold)
                            {
                                Debug.Log("OpenAndClose");
                                InteractWithCabinet(cabinetAnimator);
                            }
                            break;
                    }
                }
            }
            else if (hit.collider.CompareTag("Candle"))
            {
                //call function
                InteractCandle candleInteraction = hit.collider.gameObject.GetComponentInChildren<InteractCandle>();
                if (candleInteraction != null)
                {
                    if (Input.touchCount > 0)
                    {
                        Touch touch = Input.GetTouch(0);
                        switch (touch.phase)
                        {
                            case TouchPhase.Began:
                                touchStartPosition = touch.position;
                                touchStartTime = Time.time;
                                break;
                            case TouchPhase.Ended:
                                float touchDuration = Time.time - touchStartTime;
                                if (Vector2.Distance(touch.position, touchStartPosition) < 30f && touchDuration <= clickDurationThreshold)
                                {
                                    InteractWithCandle(candleInteraction);
                                }
                                break;
                        }
                    }
                }
                else
                {
                    Debug.LogWarning("Candle object doesn't have InteractCandle component attached.");
                }
            }
            else if (hit.collider.CompareTag("BabyPenanggal"))
            {
                // Here you can add code specific to the forEnemy tag
                Debug.Log("Detected an object with forEnemy tag!");
            }
        }

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

    public void InteractWithCabinet(TestingPlayAnimation cabinetAnimator)
    {
        if (cabinetAnimator.isOpen.Length > 0)
        {
            if (!cabinetAnimator.isOpen[0])
            {
                cabinetAnimator.isOpen[0] = true;
                Debug.Log("cabinet is open");
            }
            else
            {
                Debug.Log("cabinet is not open");
                cabinetAnimator.isOpen[0] = false;
            }
        }
        else
        {
            Debug.LogError("isOpen array is empty or not initialized!");
        }
    }

    public void InteractWithCandle(InteractCandle candleInteraction)
    {
        if (!candleInteraction.isOn[0])
        {
            candleInteraction.isOn[0] = true;
        }
        else
        {
            candleInteraction.isOn[0] = false;
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
            FindObjectOfType<AudioManager>().PlaySFX("ItemPickUp");
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

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
        //x
    }
    //private void Update()
    //{
    //    if (Input.touchCount > 0)
    //    {
    //    Touch touch = Input.GetTouch(0);
    //    if (touch.phase == TouchPhase.Began)
    //    {
    //        touchStartPosition = touch.position;
    //        touchStartTime = Time.time;
    //    }
    //    else if (touch.phase == TouchPhase.Ended && IsTouchAClick(touch.position, touchStartTime))
    //    {
    //        Debug.Log("Touch ended - processing raycast.");

    //        Ray ray = Camera.main.ScreenPointToRay(touch.position);
    //        RaycastHit hit;

    //        // Check for interactable objects first
    //        if (Physics.Raycast(ray, out hit, raycastDistance, interactableLayer))
    //        {
    //            GameObject hitObject = hit.collider.gameObject;
    //            Debug.Log("Raycast on interactable layer hit: " + hitObject.name);

    //            if (hitObject != currentInteractable)
    //            {
    //                Debug.Log("New interactable object detected.");
    //                currentInteractable = hitObject;
    //                InteractWithCurrentObject();
    //            }
    //            else
    //            {
    //                Debug.Log("Same interactable object detected - possible second click.");
    //            }
    //        }
    //        else
    //        {
    //            if (currentInteractable != null)
    //            {
    //                Debug.Log("Current interactable is no longer hit, it was: " + currentInteractable.name);
    //            }
    //            else
    //            {
    //                Debug.Log("No previous interactable to clear.");
    //            }
    //            currentInteractable = null; // Clear current interactable as nothing was hit on the interactable layer
    //        }

    //        // Now check for any other hits that should be processed regardless of the layer
    //        if (Physics.Raycast(ray, out hit, raycastDistance))
    //        {
    //            Debug.Log("Raycast on any layer hit: " + hit.collider.gameObject.name);
    //            ProcessHit(hit); // This function needs to handle hits on non-interactable objects
    //        }
    //    }
    //        if (Input.GetMouseButtonDown(0))
    //        {
    //            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //            RaycastHit hit;

    //            if (Physics.Raycast(ray, out hit, raycastDistance, interactableLayer))
    //            {
    //                ProcessHit(hit);
    //            }
    //        }
    //        DetectBabyPenanggal();
    //}

    //}
    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                touchStartPosition = touch.position;
                touchStartTime = Time.time;
            }
            else if (touch.phase == TouchPhase.Ended && IsTouchAClick(touch.position, touchStartTime))
            {
                ProcessInteraction(touch.position);
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            ProcessInteraction(Input.mousePosition);
        }

        DetectBabyPenanggal();
    }

    private void ProcessInteraction(Vector2 position)
    {
        Ray ray = Camera.main.ScreenPointToRay(position);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, raycastDistance, interactableLayer))
        {
            GameObject hitObject = hit.collider.gameObject;
            if (hitObject != currentInteractable)
            {
                Debug.Log("New interactable object detected.");
                currentInteractable = hitObject;
                InteractWithCurrentObject();
            }
            else
            {
                Debug.Log("Same interactable object detected - possible second click.");
            }
        }
        else
        {
            if (currentInteractable != null)
            {
                Debug.Log("Current interactable is no longer hit, it was: " + currentInteractable.name);
                currentInteractable = null;
            }
            else
            {
                Debug.Log("No previous interactable to clear.");
            }
        }

        if (Physics.Raycast(ray, out hit, raycastDistance))
        {
            Debug.Log("Raycast on any layer hit: " + hit.collider.gameObject.name);
            ProcessHit(hit);
        }
    }


    private bool IsTouchAClick(Vector2 touchEndPosition, float startTime)
    {
        float touchDuration = Time.time - startTime;
        return Vector2.Distance(touchEndPosition, touchStartPosition) < 30f && touchDuration <= clickDurationThreshold;
    }

    private void ProcessHit(RaycastHit hit)
    {
        if (hit.collider.CompareTag("Interactable"))
        {
            TestingPlayAnimation cabinetAnimator = hit.collider.gameObject.GetComponent<TestingPlayAnimation>();
            Debug.Log("OpenAndClose");
            InteractWithCabinet(cabinetAnimator);
        }
        else if (hit.collider.CompareTag("Candle"))
        {
            InteractCandle candleInteraction = hit.collider.gameObject.GetComponentInChildren<InteractCandle>();
            if (candleInteraction != null)
            {
                bool shouldToggle = !candleInteraction.isOn[0] && InventoryManager.Instance.GetSelectedInventoryItemName() == "Lighter";
                if (shouldToggle || candleInteraction.isOn[0])
                {
                    candleInteraction.ToggleCandle(0);
                }
            }
            else
            {
                Debug.LogWarning("Candle object doesn't have InteractCandle component attached.");
            }
        }
        else if (hit.collider.CompareTag("Note"))
        {
            Note.noteSeen = true;
        }
        else if (hit.collider.CompareTag("Door") && hit.collider.gameObject.name == "NurseryDoor")
        {
            OpenDoor.touchNurseryDoor = true;
            OpenDoor.touchDoor = false;
        }
        else if (hit.collider.CompareTag("Door") && hit.collider.gameObject.name == "BedroomDoor")
        {
            OpenDoor.touchDoor = true;
            OpenDoor.touchNurseryDoor = false;
        }
        //else if (hit.collider.CompareTag("BabyPenanggal"))
        //{
        //    hit.collider.gameObject.GetComponent<BabyPenanggal>().isSeen = true;
        //    Debug.Log("Detected an object with forEnemy tag!");
        //}
        else
        {
            currentInteractable = null;
        }
    }

    private void DetectBabyPenanggal()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        // SphereCast syntax: (Ray ray, float radius, out RaycastHit hit, float maxDistance, LayerMask layerMask)
        if (Physics.SphereCast(ray, sphereRadius, out hit, raycastDistance, LayerMask.GetMask("Default")))
        {
            if (hit.collider.CompareTag("BabyPenanggal"))
            {
                hit.collider.gameObject.GetComponent<BabyPenanggal>().isSeen = true;
                Debug.Log("Baby Penanggal detected with SphereCast!");
            }
        }
    }

    //private void CheckForButtonClick()
    //{
    //    if (Input.touchCount > 0)
    //    {
    //        Touch touch = Input.GetTouch(0);

    //        if (touch.phase == TouchPhase.Began)
    //        {
    //            Ray ray = Camera.main.ScreenPointToRay(touch.position);
    //            RaycastHit hit;

    //            if (Physics.SphereCast(ray, sphereRadius, out hit, raycastDistance, interactableLayer))
    //            {
    //                currentInteractable = hit.collider.gameObject;
    //                InteractWithCurrentObject();
    //            }
    //        }
    //    }
    //}

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
            //FindObjectOfType<AudioManager>().PlaySFX("ItemPickUp");
            IInteractableItem interactableItem = currentInteractable.GetComponent<IInteractableItem>();
            if (interactableItem != null)
            {
                string itemName = interactableItem.GetName();
                Sprite itemSprite = interactableItem.GetSprite();
                GameObject itemPrefab3D = interactableItem.GetPrefab3D();

                if (itemName == "Key")
                {
                    HandleKeyPickUp();
                }
                else if (itemName == "Lighter")
                {
                    HandleLighterPickUp();
                }
                else if(itemName == "NurseryKey")
                {
                    HandleNurseryKeyPickUp();
                }
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

    private void HandleKeyPickUp()
    {
        ScriptedEvent_4 scriptedEvent = FindObjectOfType<ScriptedEvent_4>();
        scriptedEvent.PlayRasaSayang();
        Debug.Log("Key has been picked up!");
        ObjectiveManager.objective = true;
    }

    private void HandleLighterPickUp()
    {
        ScriptedEvent_1 scriptedEvent = FindObjectOfType<ScriptedEvent_1>();
        scriptedEvent.event1();
        Debug.Log("Lighter has been picked up!");
        ItemNotice.ligterPickup = true;
        ObjectiveManager.objective = true;
    }

    private void HandleNurseryKeyPickUp()
    {
        Debug.Log("Nursery Key has been picked up!");
        ObjectiveManager.objective = true;
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

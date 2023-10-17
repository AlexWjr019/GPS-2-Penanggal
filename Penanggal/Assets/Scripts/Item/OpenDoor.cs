using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public Animation doorAnimations;
    private bool doorIsOpen;
    public string requiredItemName;
    private float raycastDistance = 3f;
    public bool door, sealDoor;

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(touch.position);

                if (Physics.Raycast(ray, out hit, raycastDistance))
                {
                    if (door)
                    {
                        InventorySlot selectedSlot = InventoryManager.Instance.selectedSlot;

                        if (selectedSlot != null && !selectedSlot.IsEmpty() && selectedSlot.GetCurrentItem().itemName == requiredItemName)
                        {
                            // Check if the raycast hit the door
                            if (hit.collider.gameObject == gameObject)
                            {
                                ToggleDoor();
                                Destroy(selectedSlot.GetCurrentItem().gameObject);
                                selectedSlot.ClearSlot();
                            }
                        }
                        else
                        {
                            ItemNotice itemNotice = FindObjectOfType<ItemNotice>();
                            if (itemNotice != null && hit.collider.gameObject.CompareTag("Door"))
                            {
                                itemNotice.ShowDoorNotice();
                            }
                            else
                            {
                                Debug.LogError("ItemNotice not found!");
                            }
                        }
                    }

                    if (sealDoor)
                    {
                        if (Seal.cursePaperDestroyed)
                        {
                            ToggleDoor();
                        }
                    }
                }
            }
        }
    }

    private void ToggleDoor()
    {
        if (!doorIsOpen)
        {
            doorIsOpen = true;
            doorAnimations.Play("OpenBedroomDoor");
        }
        else if (doorIsOpen)
        {
            doorIsOpen = false;
            doorAnimations.Play("CloseBedroomDoor");
        }
    }


}
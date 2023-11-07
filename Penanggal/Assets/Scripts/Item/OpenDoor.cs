using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public Animation doorAnimations, nurseryDoorAnima;
    private bool unlockDoor, unlockNurseryDoor;
    private bool doorIsOpen, nurseryDoorIsOpen;
    public string requiredItemName;
    private float raycastDistance = 3f;
    public bool isDoor, isNurseryDoor; // Separate flags for each door

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
                    if (isDoor) // Check the flag for the door
                    {
                        isNurseryDoor = false; // Ensure the other door is not activated

                        InventorySlot selectedSlot = InventoryManager.Instance.selectedSlot;

                        if (selectedSlot != null && !selectedSlot.IsEmpty() && selectedSlot.GetCurrentItem().itemName == requiredItemName)
                        {
                            // Check if the raycast hit the door
                            if (hit.collider.gameObject == gameObject)
                            {
                                unlockDoor = true;
                                ToggleDoor();
                                Destroy(selectedSlot.GetCurrentItem().gameObject);
                                selectedSlot.ClearSlot();
                            }
                        }
                        else if (unlockDoor)
                        {
                            ToggleDoor();
                        }
                        else
                        {
                            if (selectedSlot == null)
                            {
                                ItemNotice itemNotice = FindObjectOfType<ItemNotice>();
                                if (itemNotice != null && hit.collider.gameObject.CompareTag("Door"))
                                {
                                    FindObjectOfType<AudioManager>().PlaySFX("DoorLocked");
                                    itemNotice.ShowDoorNotice();
                                }
                                else
                                {
                                    Debug.LogError("ItemNotice not found!");
                                }
                            }

                        }
                    }
                    if (isNurseryDoor) // Check the flag for the nursery door
                    {
                        isDoor = false; // Ensure the other door is not activated

                        InventorySlot selectedSlot = InventoryManager.Instance.selectedSlot;

                        if (selectedSlot != null && !selectedSlot.IsEmpty() && selectedSlot.GetCurrentItem().itemName == requiredItemName)
                        {
                            // Check if the raycast hit the door
                            if (hit.collider.gameObject == gameObject)
                            {
                                unlockNurseryDoor = true;
                                ToggleNurseryDoor();
                                Destroy(selectedSlot.GetCurrentItem().gameObject);
                                selectedSlot.ClearSlot();
                            }
                        }
                        else if (unlockNurseryDoor)
                        {
                            ToggleNurseryDoor();
                        }
                        else
                        {
                            if(selectedSlot == null)
                            {
                                ItemNotice itemNotice = FindObjectOfType<ItemNotice>();
                                if (itemNotice != null && hit.collider.gameObject.CompareTag("Door"))
                                {
                                    FindObjectOfType<AudioManager>().PlaySFX("DoorLocked");
                                    itemNotice.ShowDoorNotice();
                                }
                                else
                                {
                                    Debug.LogError("ItemNotice not found!");
                                }
                            }
 
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
            doorAnimations.Play("OpenDoor");
            FindObjectOfType<AudioManager>().PlaySFX("DoorOpen2");
        }
        else if (doorIsOpen)
        {
            doorIsOpen = false;
            doorAnimations.Play("CloseDoor");
        }
    }

    private void ToggleNurseryDoor()
    {
        if (!nurseryDoorIsOpen)
        {
            nurseryDoorIsOpen = true;
            nurseryDoorAnima.Play("OpenNurseryDoor");
            FindObjectOfType<AudioManager>().PlaySFX("DoorOpen2");
        }
        else if (nurseryDoorIsOpen)
        {
            nurseryDoorIsOpen = false;
            nurseryDoorAnima.Play("CloseNurseryDoor");
        }
    }
}

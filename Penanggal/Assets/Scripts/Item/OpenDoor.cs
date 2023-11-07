//using UnityEngine;

//public class OpenDoor : MonoBehaviour
//{
//    public Animation doorAnimations, nurseryDoorAnima;
//    private bool unlockDoor, unlockNurseryDoor;
//    private bool doorIsOpen, nurseryDoorIsOpen;
//    public string requiredItemName;
//    private float raycastDistance = 3f;
//    public bool isDoor, isNurseryDoor; // Separate flags for each door
//    public static bool touchNurseryDoor, touchDoor;

//    private void Update()
//    {
//        if (touchNurseryDoor)
//        {
//            touchNurseryDoor = false;
//            InventorySlot selectedSlot = InventoryManager.Instance.selectedSlot;

//            if (selectedSlot != null && !selectedSlot.IsEmpty() && selectedSlot.GetCurrentItem().itemName == requiredItemName)
//            {
//                unlockNurseryDoor = true;
//                ToggleNurseryDoor();
//                Destroy(selectedSlot.GetCurrentItem().gameObject);
//                selectedSlot.ClearSlot();
//            }
//            else if (unlockNurseryDoor)
//            {
//                ToggleNurseryDoor();
//            }
//            else
//            {
//                if (selectedSlot == null)
//                {
//                    ItemNotice itemNotice = FindObjectOfType<ItemNotice>();
//                    if (itemNotice != null)
//                    {
//                        FindObjectOfType<AudioManager>().PlaySFX("DoorLocked");
//                        itemNotice.ShowDoorNotice();
//                    }
//                    else
//                    {
//                        Debug.LogError("ItemNotice not found!");
//                    }
//                }
//            }
//        }

//        if (touchDoor)
//        {
//            touchDoor = false;
//            InventorySlot selectedSlot = InventoryManager.Instance.selectedSlot;

//            if (selectedSlot != null && !selectedSlot.IsEmpty() && selectedSlot.GetCurrentItem().itemName == requiredItemName)
//            {
//                unlockDoor = true;
//                ToggleDoor();
//                Destroy(selectedSlot.GetCurrentItem().gameObject);
//                selectedSlot.ClearSlot();
//            }
//            else if (unlockDoor)
//            {
//                ToggleDoor();
//            }
//            else
//            {
//                if (selectedSlot == null)
//                {
//                    ItemNotice itemNotice = FindObjectOfType<ItemNotice>();
//                    if (itemNotice != null)
//                    {
//                        FindObjectOfType<AudioManager>().PlaySFX("DoorLocked");
//                        itemNotice.ShowDoorNotice();
//                    }
//                    else
//                    {
//                        Debug.LogError("ItemNotice not found!");
//                    }
//                }

//            }
//        }
//    }
//        //if (Input.touchCount > 0)
//        //{
//        //    Touch touch = Input.GetTouch(0);

//        //    if (touch.phase == TouchPhase.Began)
//        //    {
//        //        RaycastHit hit;
//        //        Ray ray = Camera.main.ScreenPointToRay(touch.position);

//        //        if (Physics.Raycast(ray, out hit, raycastDistance))
//        //        {
//        //            if (isDoor) // Check the flag for the door
//        //            {
//        //                isNurseryDoor = false; // Ensure the other door is not activated

//        //                InventorySlot selectedSlot = InventoryManager.Instance.selectedSlot;

//        //                if (selectedSlot != null && !selectedSlot.IsEmpty() && selectedSlot.GetCurrentItem().itemName == requiredItemName)
//        //                {
//        //                    // Check if the raycast hit the door
//        //                    if (hit.collider.gameObject.name == "BedroomDoor")
//        //                    {
//        //                        unlockDoor = true;
//        //                        ToggleDoor();
//        //                        Destroy(selectedSlot.GetCurrentItem().gameObject);
//        //                        selectedSlot.ClearSlot();
//        //                    }
//        //                }
//        //                else if (unlockDoor)
//        //                {
//        //                    ToggleDoor();
//        //                }
//        //                else
//        //                {
//        //                    if (selectedSlot == null)
//        //                    {
//        //                        ItemNotice itemNotice = FindObjectOfType<ItemNotice>();
//        //                        if (itemNotice != null && hit.collider.gameObject.CompareTag("Door"))
//        //                        {
//        //                            FindObjectOfType<AudioManager>().PlaySFX("DoorLocked");
//        //                            itemNotice.ShowDoorNotice();
//        //                        }
//        //                        else
//        //                        {
//        //                            Debug.LogError("ItemNotice not found!");
//        //                        }
//        //                    }

//        //                }
//        //            }
//        //            if (isNurseryDoor) // Check the flag for the nursery door
//        //            {
//        //                isDoor = false; // Ensure the other door is not activated

//        //                InventorySlot selectedSlot = InventoryManager.Instance.selectedSlot;

//        //                if (selectedSlot != null && !selectedSlot.IsEmpty() && selectedSlot.GetCurrentItem().itemName == requiredItemName)
//        //                {
//        //                    // Check if the raycast hit the door
//        //                    if (hit.collider.gameObject.name == "NurseryDoor")
//        //                    {
//        //                        unlockNurseryDoor = true;
//        //                        ToggleNurseryDoor();
//        //                        Destroy(selectedSlot.GetCurrentItem().gameObject);
//        //                        selectedSlot.ClearSlot();
//        //                    }
//        //                }
//        //                else if (unlockNurseryDoor)
//        //                {
//        //                    ToggleNurseryDoor();
//        //                }
//        //                else
//        //                {
//        //                    if (selectedSlot == null)
//        //                    {
//        //                        ItemNotice itemNotice = FindObjectOfType<ItemNotice>();
//        //                        if (itemNotice != null && hit.collider.gameObject.CompareTag("Door"))
//        //                        {
//        //                            FindObjectOfType<AudioManager>().PlaySFX("DoorLocked");
//        //                            itemNotice.ShowDoorNotice();
//        //                        }
//        //                        else
//        //                        {
//        //                            Debug.LogError("ItemNotice not found!");
//        //                        }
//        //                    }

//        //                }
//        //            }
//        //        }
//        //    }
//        //}


//    private void ToggleDoor()
//    {
//        if (!doorIsOpen)
//        {
//            doorIsOpen = true;
//            doorAnimations.Play("OpenDoor");
//            FindObjectOfType<AudioManager>().PlaySFX("DoorOpen2");
//        }
//        else if (doorIsOpen)
//        {
//            doorIsOpen = false;
//            doorAnimations.Play("CloseDoor");
//        }
//    }

//    private void ToggleNurseryDoor()
//    {
//        if (!nurseryDoorIsOpen)
//        {
//            nurseryDoorIsOpen = true;
//            nurseryDoorAnima.Play("OpenNurseryDoor");
//            FindObjectOfType<AudioManager>().PlaySFX("DoorOpen2");
//        }
//        else if (nurseryDoorIsOpen)
//        {
//            nurseryDoorIsOpen = false;
//            nurseryDoorAnima.Play("CloseNurseryDoor");
//        }
//    }
//}

using Unity.VisualScripting;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public Animation doorAnimations, nurseryDoorAnima;
    public string bedroomDoorRequiredItemName;
    public string nurseryDoorRequiredItemName;

    private bool bedroomDoorUnlocked = false;
    private bool nurseryDoorUnlocked = false;

    private bool doorIsOpen, nurseryDoorIsOpen;
    public bool isDoor, isNurseryDoor; // Separate flags for each door
    public static bool touchNurseryDoor, touchDoor;

    private SpawnPenanggal spawnPenanggal;

    private void Start()
    {
        spawnPenanggal = GetComponent<SpawnPenanggal>();
    }

    private void Update()
    {
        if (touchDoor)
        {
            touchDoor = false;
            InventorySlot selectedSlot = InventoryManager.Instance.selectedSlot;

            if (selectedSlot != null && !selectedSlot.IsEmpty())
            {
                if (!bedroomDoorUnlocked && selectedSlot.GetCurrentItem().itemName == bedroomDoorRequiredItemName)
                {
                    bedroomDoorUnlocked = true;
                    Destroy(selectedSlot.GetCurrentItem().gameObject);
                    selectedSlot.ClearSlot();
                    ToggleDoor();

                    spawnPenanggal.enabled = true;
                }
            }
            else if (bedroomDoorUnlocked)
            {
                ToggleDoor();
            }
            else if (!bedroomDoorUnlocked)
            {
                ShowDoorNotice("BedroomDoor");
            }
        }

        if (touchNurseryDoor)
        {
            touchNurseryDoor = false;
            InventorySlot selectedSlot = InventoryManager.Instance.selectedSlot;

            if (selectedSlot != null && !selectedSlot.IsEmpty())
            {
                if (!nurseryDoorUnlocked && selectedSlot.GetCurrentItem().itemName == nurseryDoorRequiredItemName)
                {
                    nurseryDoorUnlocked = true;
                    Destroy(selectedSlot.GetCurrentItem().gameObject);
                    selectedSlot.ClearSlot();
                    ToggleNurseryDoor();
                }
            }
            else if (nurseryDoorUnlocked)
            {
                ToggleNurseryDoor();
            }
            else if (!nurseryDoorUnlocked)
            {
                ShowDoorNotice("NurseryDoor");
            }
        }
    }

    private void ShowDoorNotice(string doorName)
    {
        ItemNotice itemNotice = FindObjectOfType<ItemNotice>();
        if (itemNotice != null)
        {
            FindObjectOfType<AudioManager>().PlaySFX("DoorLocked");
            itemNotice.ShowDoorNotice();
        }
        else
        {
            Debug.LogError("ItemNotice not found!");
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

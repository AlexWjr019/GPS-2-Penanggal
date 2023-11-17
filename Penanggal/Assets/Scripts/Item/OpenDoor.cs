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
            else if (!bedroomDoorUnlocked || (!bedroomDoorUnlocked && selectedSlot.GetCurrentItem().itemName != "Key"))
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
            else if (!nurseryDoorUnlocked || (!nurseryDoorUnlocked && selectedSlot.GetCurrentItem().itemName != "NurseryKey"))
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

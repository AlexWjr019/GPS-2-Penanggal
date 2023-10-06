using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public Animation doorAnimations;
    private bool doorIsOpen;
    public string requiredItemName;

    private void OnMouseDown()
    {
        InventorySlot selectedSlot = InventoryManager.Instance.selectedSlot;

        if (selectedSlot != null &&!selectedSlot.IsEmpty() && selectedSlot.GetCurrentItem().itemName == requiredItemName)
        {
            ToggleDoor();

            Destroy(selectedSlot.GetCurrentItem().gameObject);
            selectedSlot.ClearSlot();
        }
    }

    private void ToggleDoor()
    {
        if (!doorIsOpen)
        {
            doorIsOpen = true;
            doorAnimations.Play("OpenDoor");
        }
        else if (doorIsOpen)
        {
            doorIsOpen = false;
            doorAnimations.Play("CloseDoor");
        }
    }
}

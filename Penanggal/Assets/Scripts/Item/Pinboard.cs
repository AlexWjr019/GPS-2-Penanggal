using UnityEngine;

public class Pinboard : MonoBehaviour
{
    public string requiredItemName;
    private float raycastDistance = 3f;
    public GameObject pinPaper;

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
                    InventorySlot selectedSlot = InventoryManager.Instance.selectedSlot;

                    if (selectedSlot != null && !selectedSlot.IsEmpty() && selectedSlot.GetCurrentItem().itemName == requiredItemName)
                    {
                        // Check if the raycast hit the door
                        if (hit.collider.gameObject == gameObject)
                        {
                            pinPaper.SetActive(true);
                            Destroy(selectedSlot.GetCurrentItem().gameObject);
                            selectedSlot.ClearSlot();
                        }
                    }
                    else
                    {
                        ItemNotice itemNotice = FindObjectOfType<ItemNotice>();
                        if (itemNotice != null && hit.collider.gameObject.CompareTag("Pinboard"))
                        {
                            //itemNotice.ShowDoorNotice();
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
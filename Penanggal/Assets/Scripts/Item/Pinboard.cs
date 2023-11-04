using System.Collections;
using UnityEngine;

public class Pinboard : MonoBehaviour
{
    public string requiredItemName;
    private float raycastDistance = 5f;
    public GameObject pinPaper;
    public GameObject cursePaper;

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
                            StartCoroutine(SpawnCursepaper());
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

    IEnumerator SpawnCursepaper()
    {
        yield return new WaitForSeconds(1.0f);
        cursePaper.SetActive(true);
        pinPaper.SetActive(false);
    }
}
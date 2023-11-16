using System.Collections;
using UnityEngine;

public class Pinboard : MonoBehaviour
{
    public string requiredItemName;
    private float raycastDistance = 5f;
    public GameObject pinPaper;
    public GameObject cursePaper;
    public static bool cursePaperburn;

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
                        // Check if the raycast hit the pinboard
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
                            itemNotice.ShowPinboardNotice();
                        }
                        else
                        {
                            Debug.LogError("ItemNotice not found!");
                        }
                    }
                }
            }
        }

        if (cursePaperburn)
        {
            cursePaperburn = false;
            LevelChanger levelChange = FindObjectOfType<LevelChanger>();
            if (levelChange != null)
            {
                levelChange.FadeToNextLevel();
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
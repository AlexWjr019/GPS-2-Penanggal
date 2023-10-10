using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.ProBuilder.Shapes;
using UnityEngine.UI;

public class FlameObject : MonoBehaviour
{
    public GameObject particleSystemObject;
    public Image fillImage;
    //private float fillSpeed = 0.5f;
    //private bool isCursePaperClicked = false;
    public Seal seal;
    public string requiredItemName;
    private float raycastDistance = 3f;
    public GameObject cursepaper;

    private void Update()
    {
        //// Check if cursePaper is true and the fill amount is greater than 0
        //if (isCursePaperClicked && fillImage != null && fillImage.fillAmount > 0)
        //{
        //    // Decrease the fill amount over time
        //    fillImage.fillAmount -= fillSpeed * Time.deltaTime;

        //    // If the fill amount reaches 0, destroy the canvas
        //    if (fillImage.fillAmount <= 0)
        //    {
        //        Destroy(gameObject);
        //        Seal seal = FindObjectOfType<Seal>();
        //        seal.OnCursePaperDestroyed();
        //    }
        //}

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;

                // Check if the ray hits any GameObject
                if (Physics.Raycast(ray, out hit, raycastDistance))
                {
                    InventorySlot selectedSlot = InventoryManager.Instance.selectedSlot;
                    GameObject clickedObject2 = hit.collider.gameObject;
                    if (selectedSlot != null && !selectedSlot.IsEmpty() && selectedSlot.GetCurrentItem().itemName == requiredItemName)
                    {
                        GameObject clickedObject = hit.collider.gameObject;
                        // Check if the clicked GameObject is the candle
                        if (clickedObject.CompareTag("Candle"))
                        {
                            if (particleSystemObject != null)
                            {
                                particleSystemObject.SetActive(true);
                            }
                        }
                        // Check if the clicked GameObject is the cursePaper
                        else if (clickedObject.CompareTag("CursePaper"))
                        {
                            Destroy(cursepaper);
                            //isCursePaperClicked = true; // Mark that cursePaper has been clicked
                        }

                    }

                    else if (clickedObject2.CompareTag("Candle") || clickedObject2.CompareTag("CursePaper"))
                    {
                        if (selectedSlot == null || selectedSlot.IsEmpty() || selectedSlot.GetCurrentItem().itemName != requiredItemName)
                        {
                            Debug.Log("No lighter found!");
                            ItemNotice itemNotice = FindObjectOfType<ItemNotice>();
                            if (itemNotice != null)
                            {
                                itemNotice.ShowFlameNotice();
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

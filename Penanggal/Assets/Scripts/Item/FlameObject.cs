using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FlameObject : MonoBehaviour
{
    public string requiredItemName;
    private float raycastDistance = 10f;
    private List<GameObject> cursepapers = new List<GameObject>();
    private int currentCursePaperIndex = 0;
    private int cursePapernum = 4;

    public GameObject[] cursePaperObjects;

    private bool toiletCursepaper;
    public GameObject number4, number3, number2, number1;
    public GameObject nurseryKey;
    public GameObject bedroomKey;

    public ScriptedEvent_7 scriptedEvent7;
    public ScriptedEvent_8 ScriptedEvent8;

    private bool objectiveActive = true;

    private void Start()
    {
        // Find all curse paper objects with the "CursePaper" tag and add them to the list.
        cursePaperObjects = GameObject.FindGameObjectsWithTag("CursePaper");
        cursepapers.AddRange(cursePaperObjects);
        toiletCursepaper = false;
    }

    private void Update()
    {
        //if (Input.touchCount > 0)
        //{
        //    Touch touch = Input.GetTouch(0);

        //    if (touch.phase == TouchPhase.Began)
        //    {
        //        Ray ray = Camera.main.ScreenPointToRay(touch.position);
        //        RaycastHit hit;

        //        // Check if the ray hits any GameObject
        //        if (Physics.Raycast(ray, out hit, raycastDistance))
        //        {
        //            InventorySlot selectedSlot = InventoryManager.Instance.selectedSlot;
        //            GameObject clickedObject2 = hit.collider.gameObject;
        //            if (selectedSlot != null && !selectedSlot.IsEmpty() && selectedSlot.GetCurrentItem().itemName == requiredItemName)
        //            {
        //                Debug.Log("Item matches the required item: " + requiredItemName);
        //                GameObject clickedObject = hit.collider.gameObject;
        //                // Check if the clicked GameObject is the cursePaper
        //                if (clickedObject.CompareTag("CursePaper"))
        //                {
        //                    // Iterate through the list of curse papers
        //                    for (int i = 0; i < cursepapers.Count; i++)
        //                    {
        //                        // Check if the clicked object matches the current curse paper
        //                        if (cursepapers[i] == clickedObject)
        //                        {
        //                            // Destroy the matching curse paper
        //                            FindObjectOfType<AudioManager>().PlaySFX("BurningPaperSound");
        //                            Destroy(cursepapers[i]);
        //                            cursepapers.RemoveAt(i); // Remove it from the list
        //                            cursePapernum--;
        //                            ItemNotice itemNotice = FindObjectOfType<ItemNotice>();
        //                            if (itemNotice != null)
        //                            {
        //                                itemNotice.ShowBurnedCursePaperNotice(cursePapernum);
        //                            }
        //                            if (clickedObject.name == "TornCursePaper")
        //                            {
        //                                Pinboard.cursePaperburn = true;
        //                            }
        //                            if(clickedObject.name == "Curse Paper_1")
        //                            {
        //                                toiletCursepaper = true;
        //                                number4.SetActive(false);
        //                                number3.SetActive(true);

        //                            }
        //                            if (clickedObject.name == "Curse Paper_2")
        //                            {
        //                                toiletCursepaper = true;
        //                                number3.SetActive(false);
        //                                number2.SetActive(true);
        //                                nurseryKey.SetActive(true);
        //                                ObjectiveManager.objective = true;

        //                            }
        //                            if (clickedObject.name == "TornCursePaper")
        //                            {
        //                                toiletCursepaper = true;
        //                                number3.SetActive(false);
        //                                number2.SetActive(true);

        //                            }
        //                            if(clickedObject.name == "LastCursePaper")
        //                            {
        //                                ObjectiveManager2.objective = true;
        //                                scriptedEvent7.PlayScriptedEvent7();
        //                                //SceneManager.LoadScene("WinScreen");
        //                            }
        //                            return; // Exit the loop
        //                        }
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                if ((clickedObject2.CompareTag("CursePaper") && selectedSlot == null) || (clickedObject2.CompareTag("CursePaper") && selectedSlot.IsEmpty()) || (clickedObject2.CompareTag("CursePaper") && selectedSlot.GetCurrentItem().itemName != "Lighter"))
        //                {
        //                    // Player does not have the required item, show the notice
        //                    ItemNotice itemNotice = FindObjectOfType<ItemNotice>();
        //                    if (itemNotice != null)
        //                    {
        //                        itemNotice.ShowFlameNotice();
        //                    }
        //                }

        //                if ((clickedObject2.CompareTag("Candle") && selectedSlot == null) || (clickedObject2.CompareTag("Candle") && selectedSlot.IsEmpty()) || (clickedObject2.CompareTag("Candle") && selectedSlot.GetCurrentItem().itemName != "Lighter"))
        //                {
        //                    // Player does not have the required item, show the notice
        //                    ItemNotice itemNotice = FindObjectOfType<ItemNotice>();
        //                    if (itemNotice != null)
        //                    {
        //                        itemNotice.ShowFlameNotice();
        //                    }
        //                }
        //            }
        //        }

        //    }
        //}
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, raycastDistance))
                {
                    HandleInteraction(hit);
                }
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, raycastDistance))
            {
                HandleInteraction(hit);
            }
        }

        if (toiletCursepaper)
        {
            ScriptedEvent_6 event6 = FindObjectOfType<ScriptedEvent_6>();
            if (event6 != null)
            {
                event6.PlayWindowShatter();
                toiletCursepaper = false;
            }
        }
    }

    private void HandleInteraction(RaycastHit hit)
    {
        InventorySlot selectedSlot = InventoryManager.Instance.selectedSlot;
        GameObject clickedObject2 = hit.collider.gameObject;
        if (selectedSlot != null && !selectedSlot.IsEmpty() && selectedSlot.GetCurrentItem().itemName == requiredItemName)
        {
            Debug.Log("Item matches the required item: " + requiredItemName);
            GameObject clickedObject = hit.collider.gameObject;
            // Check if the clicked GameObject is the cursePaper
            if (clickedObject.CompareTag("CursePaper"))
            {
                // Iterate through the list of curse papers
                for (int i = 0; i < cursepapers.Count; i++)
                {
                    // Check if the clicked object matches the current curse paper
                    if (cursepapers[i] == clickedObject)
                    {
                        ItemNotice itemNotice = FindObjectOfType<ItemNotice>();

                        if (clickedObject.name == "TornCursePaper")
                        {
                            Pinboard.cursePaperburn = true;
                        }
                        if (clickedObject.name == "Curse Paper_1")
                        {
                            // Destroy the matching curse paper
                            FindObjectOfType<AudioManager>().PlaySFX("BurningPaperSound");
                            Destroy(cursepapers[i]);
                            cursepapers.RemoveAt(i); // Remove it from the list
                            toiletCursepaper = true;
                            number4.SetActive(false);
                            number3.SetActive(true);
                            bedroomKey.SetActive(true);
                            cursePapernum--;
                            if (itemNotice != null)
                            {
                                itemNotice.ShowBurnedCursePaperNotice(cursePapernum);
                            }
                            if (objectiveActive)
                            {
                                ObjectiveManager.objective = true;
                                objectiveActive = false;
                            }

                        }
                        if (clickedObject.name == "Curse Paper_2")
                        {
                            if (LockControl.safeIsOpen)
                            {
                                // Destroy the matching curse paper
                                FindObjectOfType<AudioManager>().PlaySFX("BurningPaperSound");
                                Destroy(cursepapers[i]);
                                cursepapers.RemoveAt(i); // Remove it from the list
                                toiletCursepaper = true;
                                number3.SetActive(false);
                                number2.SetActive(true);
                                nurseryKey.SetActive(true);
                                ObjectiveManager.objective = true;
                                cursePapernum--;
                                if (itemNotice != null)
                                {
                                    itemNotice.ShowBurnedCursePaperNotice(cursePapernum);
                                }
                            }
                        }
                        if (clickedObject.name == "TornCursePaper")
                        {
                            // Destroy the matching curse paper
                            FindObjectOfType<AudioManager>().PlaySFX("BurningPaperSound");
                            Destroy(cursepapers[i]);
                            cursepapers.RemoveAt(i); // Remove it from the list
                            toiletCursepaper = true;
                            number3.SetActive(false);
                            number2.SetActive(true);
                            cursePapernum--;
                            if (itemNotice != null)
                            {
                                itemNotice.ShowBurnedCursePaperNotice(cursePapernum);
                            }

                        }
                        if (clickedObject.name == "LastCursePaper")
                        {
                            // Destroy the matching curse paper
                            FindObjectOfType<AudioManager>().PlaySFX("BurningPaperSound");
                            Destroy(cursepapers[i]);
                            cursepapers.RemoveAt(i); // Remove it from the list
                            ObjectiveManager2.objective = true;
                            scriptedEvent7.PlayScriptedEvent7();
                            //SceneManager.LoadScene("WinScreen");
                        }
                        return; // Exit the loop
                    }
                }
            }
        }
        else
        {
            if ((clickedObject2.CompareTag("CursePaper") && selectedSlot == null) || (clickedObject2.CompareTag("CursePaper") && selectedSlot.IsEmpty()) || (clickedObject2.CompareTag("CursePaper") && selectedSlot.GetCurrentItem().itemName != "Lighter"))
            {
                // Player does not have the required item, show the notice
                ItemNotice itemNotice = FindObjectOfType<ItemNotice>();
                if (itemNotice != null)
                {
                    itemNotice.ShowFlameNotice();
                }
            }

            if ((clickedObject2.CompareTag("Candle") && selectedSlot == null) || (clickedObject2.CompareTag("Candle") && selectedSlot.IsEmpty()) || (clickedObject2.CompareTag("Candle") && selectedSlot.GetCurrentItem().itemName != "Lighter"))
            {
                // Player does not have the required item, show the notice
                ItemNotice itemNotice = FindObjectOfType<ItemNotice>();
                if (itemNotice != null)
                {
                    itemNotice.ShowLightUpNotice();
                }
            }
        }
    }
}
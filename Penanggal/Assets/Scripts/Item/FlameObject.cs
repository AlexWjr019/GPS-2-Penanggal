using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FlameObject : MonoBehaviour, IPointerClickHandler
{
    public GameObject particleSystemObject;
    public Image fillImage;
    public Canvas canvasToDestroy;
    private float fillSpeed = 0.5f;
    private bool isCursePaperClicked = false;

    private void Update()
    {
        // Check if cursePaper is true and the fill amount is greater than 0
        if (isCursePaperClicked && fillImage != null && fillImage.fillAmount > 0)
        {
            // Decrease the fill amount over time
            fillImage.fillAmount -= fillSpeed * Time.deltaTime;

            // If the fill amount reaches 0, destroy the canvas
            if (fillImage.fillAmount <= 0)
            {
                DestroyCanvas();
            }
        }
    }

    private void OnMouseDown()
    {
        // Create a ray from the mouse position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Check if the ray hits any GameObject
        if (Physics.Raycast(ray, out hit))
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
                isCursePaperClicked = true; // Mark that cursePaper has been clicked
            }
        }
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        // Check if the GameObject that was clicked is the candle
        if (eventData.pointerCurrentRaycast.gameObject.CompareTag("Candle"))
        {
            if (particleSystemObject != null)
            {
                particleSystemObject.SetActive(true);
            }
        }
        // Check if the GameObject that was clicked is the cursePaper
        if (eventData.pointerCurrentRaycast.gameObject.CompareTag("CursePaper"))
        {
            isCursePaperClicked = true; // Mark that cursePaper has been clicked

            // Prevent decreasing the fill amount of cursePaper when clicking the candle
            if (fillImage != null && fillImage.fillAmount > 0)
            {
                fillImage.fillAmount -= fillSpeed * Time.deltaTime;
                if (fillImage.fillAmount <= 0)
                {
                    DestroyCanvas();
                }
            }
        }
    }

    private void DestroyCanvas()
    {
        if (canvasToDestroy != null)
        {
            Destroy(canvasToDestroy.gameObject);
        }
        else
        {
            Debug.LogError("Canvas to destroy is not assigned.");
        }
    }
}

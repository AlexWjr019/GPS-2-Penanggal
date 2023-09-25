using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Key : MonoBehaviour
{
    public GameObject key;
    public static bool getKey;

    private void OnMouseDown()
    {
        // Create a ray from the mouse position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Check if the ray hits any GameObject
        if (Physics.Raycast(ray, out hit))
        {
            GameObject clickedObject = hit.collider.gameObject;

            // Check if the clicked GameObject is the door
            if (clickedObject.CompareTag("Key"))
            {
                Destroy(key);
                getKey = true;
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Check if the GameObject that was clicked is the candle
        if (eventData.pointerCurrentRaycast.gameObject.CompareTag("Key"))
        {
            Destroy(key);
            getKey = true;
        }
    }
}

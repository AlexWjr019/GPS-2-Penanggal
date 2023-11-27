using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OutlineSelect : MonoBehaviour
{
    private Transform highlight;
    private Transform selection;
    private RaycastHit raycastHit;
    public Camera playerCamera; // Reference to the main player camera
    public float sphereCastRadius = 0.5f; // Radius of the SphereCast
    public float maxDistance = 5f; // Maximum distance of the SphereCast

    void Update()
    {
        RaycastHit hit;

        // Highlight
        if (highlight != null)
        {
            highlight.gameObject.GetComponent<Outline>().enabled = false;
            highlight = null;
        }

        if (Physics.SphereCast(playerCamera.transform.position, sphereCastRadius, playerCamera.transform.forward, out hit, maxDistance))
        {
            Transform newHighlight = hit.transform;

            if (newHighlight.CompareTag("CursePaper"))
            {
                // Activate outline if it already exists, otherwise create a new one
                Outline outline = newHighlight.gameObject.GetComponent<Outline>();
                if (outline == null)
                {
                    outline = newHighlight.gameObject.AddComponent<Outline>();
                    outline.OutlineColor = Color.white;
                    outline.OutlineWidth = 7.0f;
                }

                // Activate the outline
                outline.enabled = true;

                // Set the current highlighted object
                highlight = newHighlight;
            }
        }
    }
}

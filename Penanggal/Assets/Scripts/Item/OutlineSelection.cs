using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OutlineSelection : MonoBehaviour
{
    public Camera playerCamera; // Reference to the main player camera
    public float sphereCastRadius = 0.5f; // Radius of the SphereCast
    public float maxDistance = 5f; // Maximum distance of the SphereCast
    private ItemOutline outlineScript;
    private bool isOutlined = false;
    private bool outline = true;
    //public float offsetDistance = -5f;

    private void Start()
    {
        outlineScript = GetComponent<ItemOutline>();
        outlineScript.enabled = false;
    }

    private void Update()
    {
        CheckItemHighlight();

        if (Hide.isHide)
        {
            DeactivateOutline();
        }
    }

    private void CheckItemHighlight()
    {
        RaycastHit hit;
        int combinedLayerMask = LayerMask.GetMask("Items") | LayerMask.GetMask("Hide") | LayerMask.GetMask("Door");
        // Calculate the SphereCast origin based on the player's view position
        //Vector3 sphereCastOrigin = playerCamera.transform.position + playerCamera.transform.forward * offsetDistance;

        // Perform the SphereCast
        if (Physics.SphereCast(playerCamera.transform.position, sphereCastRadius, playerCamera.transform.forward, out hit, maxDistance, combinedLayerMask))
        {
            Debug.DrawRay(playerCamera.transform.position, playerCamera.transform.forward * maxDistance, Color.blue);

            if (hit.collider.gameObject == this.gameObject)
            {
                // If the SphereCast hit this item and it's not already outlined
                if (!isOutlined)
                {
                    Debug.Log("Activating Outline");
                    ActivateOutline();
                }
            }
            else
            {
                // If the SphereCast hit another item and this item is outlined
                if (isOutlined)
                {
                    Debug.Log("Deactivating Outline");
                    DeactivateOutline();
                }
            }
        }
        else if (isOutlined)
        {
            Debug.Log("Deactivating Outline (no hit)");
            DeactivateOutline();
        }
    }




    private void ActivateOutline()
    {
        outlineScript.enabled = true;
        isOutlined = true;
    }

    private void DeactivateOutline()
    {
        outlineScript.enabled = false;
        isOutlined = false;
    }
}


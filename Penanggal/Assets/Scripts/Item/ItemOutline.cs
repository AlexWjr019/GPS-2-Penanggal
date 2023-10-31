using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemOutline : MonoBehaviour
{
    public Shader outlineShader; // Reference to the outline shader.
    public Color outlineColor = Color.white; // Color for the outline.
    public float outlineWidth = 1.03f; // Width of the outline.

    private Material outlineMaterial;
    private List<Renderer> outlinedRenderers = new List<Renderer>();
    private Dictionary<Renderer, Material[]> originalMaterials = new Dictionary<Renderer, Material[]>();

    public Camera playerCamera; // Reference to the main player camera
    public float sphereCastRadius = 0.5f; // Radius of the SphereCast
    public float maxDistance = 3f; // Maximum distance of the SphereCast

    private void Start()
    {
        outlineMaterial = new Material(outlineShader);
        outlineMaterial.hideFlags = HideFlags.HideAndDontSave; // Hide the material in the Inspector.
    }

    private void Update()
    {
        // Check if the script is enabled before proceeding.
        if (enabled)
        {
            RaycastHit hit;

            if (Physics.SphereCast(playerCamera.transform.position, sphereCastRadius, playerCamera.transform.forward, out hit, maxDistance, LayerMask.GetMask("Items")))
            {
                Debug.DrawRay(playerCamera.transform.position, playerCamera.transform.forward * maxDistance, Color.red);
                Transform hitTransform = hit.transform;

                // Apply the outline to the hierarchy of the hit object.
                ApplyOutlineToHierarchy(hitTransform);
            }
            else
            {
                if (enabled == false)
                {
                    // Remove the renderer from the list and restore its original materials.
                    foreach (Renderer rend in outlinedRenderers)
                    {
                        if (originalMaterials.ContainsKey(rend))
                        {
                            // Restore the original materials.
                            rend.materials = originalMaterials[rend];
                            originalMaterials.Remove(rend);
                        }
                    }
                    outlinedRenderers.Clear(); // Clear the list of outlined renderers.
                }
            }
        }
    }

    void OnDisable()
    {
        // Remove the renderer from the list and restore its original materials.
        foreach (Renderer rend in outlinedRenderers)
        {
            if (originalMaterials.ContainsKey(rend))
            {
                // Restore the original materials.
                rend.materials = originalMaterials[rend];
                originalMaterials.Remove(rend);
            }
        }
        outlinedRenderers.Clear(); // Clear the list of outlined renderers.
    }

    // Recursive function to apply outline to the entire hierarchy.
    void ApplyOutlineToHierarchy(Transform transform)
    {
        Renderer rend = transform.GetComponent<Renderer>();

        if (rend != null && !outlinedRenderers.Contains(rend))
        {
            // Store the original materials.
            originalMaterials[rend] = rend.materials;

            // Create a new materials array to include the outline material.
            Material[] materials = new Material[rend.materials.Length + 1];

            // Copy the original materials.
            for (int i = 0; i < rend.materials.Length; i++)
            {
                materials[i] = rend.materials[i];
            }

            // Assign the outline material to the last element.
            materials[materials.Length - 1] = outlineMaterial;

            // Assign the new materials array to the renderer.
            rend.materials = materials;

            // Set the outline material properties.
            outlineMaterial.SetColor("_OutlineColor", outlineColor);
            outlineMaterial.SetFloat("_OutlineWidth", outlineWidth);

            // Add the renderer to the list of outlined renderers.
            outlinedRenderers.Add(rend);
        }

        // Recursively apply outline to children.
        foreach (Transform child in transform)
        {
            ApplyOutlineToHierarchy(child);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryItems : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("UI")]
    public Image image;
    public GameObject interactedSphere;
    public Transform canvasTransform;

    [HideInInspector] public Transform parentAfterDrag;

    public void ShowInteractedSphere()
    {
        if (interactedSphere != null)
        {
            MeshRenderer sphereRenderer = interactedSphere.GetComponent<MeshRenderer>();
            if (sphereRenderer != null)
            {
                Material sphereMaterial = sphereRenderer.material;
                if (sphereMaterial != null)
                {
                    image.sprite = Sprite.Create(Texture2D.whiteTexture, new Rect(0, 0, 1, 1), Vector2.zero);
                    image.material = sphereMaterial;
                }
            }
        }
    }
    //drag and drop
    public void OnBeginDrag(PointerEventData eventData)
    {
        image.raycastTarget = false;
        parentAfterDrag = transform.parent;
        transform.SetParent(canvasTransform);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        image.raycastTarget = true;
        transform.SetParent(parentAfterDrag);
    }
}

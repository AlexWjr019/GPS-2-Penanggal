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

    void Start()
    {
        canvasTransform = FindObjectOfType<Canvas>().transform;
    }

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
                    image.sprite = Sprite.Create(Texture2D.whiteTexture, new Rect(0, 0, 1, 1), new Vector2(0.5f, 0.5f));
                    image.material = sphereMaterial;
                }
            }
        }
    }

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

    // 我理解你想要的是"Configure"方法替换"SetupItem"方法，所以在这里我将"SetupItem"替换成"Configure"
    public void Configure(string itemName, /*Material itemMaterial*/ Sprite itemSprite)
    {
        //if (itemMaterial != null)
        //{
        //    image.material = itemMaterial;
        //}

        if (itemSprite != null)
        {
            image.sprite = itemSprite; 
        }
    }
}

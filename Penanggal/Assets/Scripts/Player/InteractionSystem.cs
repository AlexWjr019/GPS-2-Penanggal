using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InteractionSystem : MonoBehaviour
{
    public LayerMask interactableLayer;
    public GameObject interactionUI;
    private GameObject currentInteractable;
    public InventoryItems inventoryItemPrefab; // 引用你的 InventoryItems Prefab

    private float raycastDistance = 3f;

    private void Update()
    {
        Vector3 cameraPosition = Camera.main.transform.position;
        Vector3 cameraForward = Camera.main.transform.forward;
        Ray ray = new Ray(cameraPosition, cameraForward);

        Debug.DrawRay(ray.origin, ray.direction * raycastDistance, Color.blue, 1f);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, raycastDistance, interactableLayer))
        {
            GameObject hitObject = hit.collider.gameObject;

            if (hitObject != currentInteractable)
            {
                HideInteractionUI();

                ShowInteractionUI(hitObject);

                currentInteractable = hitObject;
            }
            else
            {
                CheckForButtonClick();
            }
        }
        else
        {
            HideInteractionUI();
            currentInteractable = null;
        }
    }

    private void ShowInteractionUI(GameObject interactableObject)
    {
        if (interactionUI != null)
        {
            interactionUI.SetActive(true);
        }
    }

    private void HideInteractionUI()
    {
        if (interactionUI != null)
        {
            interactionUI.SetActive(false);
        }
    }

    private void CheckForButtonClick()
    {
        if (Input.GetButtonDown("Button1"))
        {
            Debug.Log("Button1 clicked!");
            InteractWithCurrentObject();
        }
    }


    public void InteractWithCurrentObject()
    {
        if (currentInteractable != null)
        {
            // 如果有空的 InventorySlot，生成 InventoryItems Prefab 并放入其中
            InventorySlot emptySlot = InventoryManager.Instance.GetEmptySlot();
            if (emptySlot != null)
            {
                // 创建 InventoryItems Prefab 并放入 InventorySlot 的子对象
                InventoryItems newInventoryItem = Instantiate(inventoryItemPrefab, emptySlot.transform);
                newInventoryItem.transform.localPosition = Vector3.zero;
                newInventoryItem.transform.localScale = Vector3.one;
                newInventoryItem.name = "InventoryItem"; // 设置名称
                newInventoryItem.gameObject.SetActive(true); // 激活物体

                // 执行 Interactable 对象的交互行为
                currentInteractable.SetActive(false);

                // 隐藏 InteractionUI
                HideInteractionUI();
            }
            else
            {
                Debug.LogError("inventoryItemPrefab is not assigned!");
                Debug.LogWarning("没有可用的 InventorySlot 来放置物品！");
            }
        }
    }
}

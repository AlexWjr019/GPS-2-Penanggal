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
    public InventoryItems inventoryItemPrefab; // ������� InventoryItems Prefab

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
            // ����пյ� InventorySlot������ InventoryItems Prefab ����������
            InventorySlot emptySlot = InventoryManager.Instance.GetEmptySlot();
            if (emptySlot != null)
            {
                // ���� InventoryItems Prefab ������ InventorySlot ���Ӷ���
                InventoryItems newInventoryItem = Instantiate(inventoryItemPrefab, emptySlot.transform);
                newInventoryItem.transform.localPosition = Vector3.zero;
                newInventoryItem.transform.localScale = Vector3.one;
                newInventoryItem.name = "InventoryItem"; // ��������
                newInventoryItem.gameObject.SetActive(true); // ��������

                // ִ�� Interactable ����Ľ�����Ϊ
                currentInteractable.SetActive(false);

                // ���� InteractionUI
                HideInteractionUI();
            }
            else
            {
                Debug.LogError("inventoryItemPrefab is not assigned!");
                Debug.LogWarning("û�п��õ� InventorySlot ��������Ʒ��");
            }
        }
    }
}

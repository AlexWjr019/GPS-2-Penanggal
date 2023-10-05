using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hide : MonoBehaviour
{
    public bool isHide;

    public GameObject hideCamera;
    public GameObject player;
    public LayerMask interactableLayer;
    private GameObject currentInteractable;
    private float raycastDistance = 3f;

    private void Start()
    {
        isHide = false;
        hideCamera.SetActive(false);
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                if(isHide == false)
                {
                    Ray ray = Camera.main.ScreenPointToRay(touch.position);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit, raycastDistance, interactableLayer))
                    {
                        currentInteractable = hit.collider.gameObject;

                        if (currentInteractable.CompareTag("Hide"))
                        {
                            // Disable Player Camera
                            hideCamera.SetActive(true);
                            player.SetActive(false);
                            isHide = true;
                        }
                    }
                }
                else
                {
                    Ray ray = hideCamera.GetComponent<Camera>().ScreenPointToRay(touch.position);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit, raycastDistance, interactableLayer))
                    {
                        currentInteractable = hit.collider.gameObject;

                        if (currentInteractable.CompareTag("Hide"))
                        {
                            // Disable Player Camera
                            hideCamera.SetActive(false);
                            player.SetActive(true);
                            isHide = false;
                        }
                    }
                }
            }
        }
    }



}

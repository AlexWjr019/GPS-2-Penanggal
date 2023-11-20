using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoOpenDoor : MonoBehaviour
{
    private bool isTriggered;
    public Animation nurseryDoorAnima;
    private bool isOpen;

    private void Start()
    {
        isOpen = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (isOpen)
            {
                isTriggered = true;
            }
           
        }
    }

    void Update()
    {
        if(isTriggered)
        {
            Debug.Log("Player Enter Nursery Room");
            nurseryDoorAnima.Play("CloseNurseryDoor");
            isOpen = false;
            isTriggered = false;
        }   
    }
}

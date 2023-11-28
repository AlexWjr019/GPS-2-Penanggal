using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleDetect3 : MonoBehaviour
{
    public static bool isTriggered;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Flower"))
        {
            
            isTriggered = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Flower"))
        {
            isTriggered = false;
        }
    }

    private void Update()
    {
        Debug.Log("flower " + isTriggered);
    }
}

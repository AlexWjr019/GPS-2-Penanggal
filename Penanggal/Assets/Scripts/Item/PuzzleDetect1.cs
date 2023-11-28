using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleDetect1 : MonoBehaviour
{
    public static bool isTriggered;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Bottle"))
        {
            
            isTriggered = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Bottle"))
        {
            isTriggered = false;
        }
    }

    private void Update()
    {
        Debug.Log("bottle " + isTriggered);
    }
}

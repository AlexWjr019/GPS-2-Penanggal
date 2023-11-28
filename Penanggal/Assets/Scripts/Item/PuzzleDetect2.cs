using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleDetect2 : MonoBehaviour
{
    public static bool isTriggered;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Bowl"))
        {
            
            isTriggered = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Bowl"))
        {
            isTriggered = false;
        }
    }

    private void Update()
    {
        Debug.Log("bowl " + isTriggered);
    }
}

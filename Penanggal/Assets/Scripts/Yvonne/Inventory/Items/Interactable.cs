using UnityEngine;

public class Interactable : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Debug.Log("player detected");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("player exited");
    }
}

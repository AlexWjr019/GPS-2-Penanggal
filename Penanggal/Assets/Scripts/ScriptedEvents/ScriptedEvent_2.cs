using UnityEngine;

public class ScriptedEvent_2 : MonoBehaviour
{
    public bool isTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if(!isTriggered)
        {
            FindObjectOfType<AudioManager>().PlaySFX("Crying");
            isTriggered = true;
        }
    }
}

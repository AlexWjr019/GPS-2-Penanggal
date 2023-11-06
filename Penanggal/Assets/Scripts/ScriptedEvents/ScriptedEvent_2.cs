using UnityEngine;

public class ScriptedEvent_2 : MonoBehaviour
{
    public AudioSource womenCrying;
    public bool isTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if(!isTriggered)
        {
            womenCrying.Play();
            isTriggered = true;
        }
    }
}

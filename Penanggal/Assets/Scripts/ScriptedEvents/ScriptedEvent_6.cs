using UnityEngine;

public class ScriptedEvent_6 : MonoBehaviour
{
    public AudioSource windowShatter;
    public bool isTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        PlayWindowShatter();
    }

    public void PlayWindowShatter()
    {
        if (!isTriggered)
        {
            windowShatter.Play();
            isTriggered = true;
        }
    }
}

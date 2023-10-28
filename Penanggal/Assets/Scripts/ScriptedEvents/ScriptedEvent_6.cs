using UnityEngine;

public class ScriptedEvent_6 : MonoBehaviour
{
    public bool isTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        PlayWindowShatter();
    }

    public void PlayWindowShatter()
    {
        if (!isTriggered)
        {
            FindObjectOfType<AudioManager>().PlaySFX("WindowShatter");
            isTriggered = true;
        }
    }
}

using UnityEngine;

public class ScriptedEvent_4 : MonoBehaviour
{
    public bool isTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        PlayRasaSayang();
    }

    public void PlayRasaSayang()
    {
        if(!isTriggered)
        {
            FindObjectOfType<AudioManager>().PlayMusic("RasaSayang");
            isTriggered = true;
        }
    }
}

using UnityEngine;

public class ScriptedEvent_4 : MonoBehaviour
{
    public void PlayRasaSayang()
    {
        Debug.Log("its work");
        FindObjectOfType<AudioManager>().PlayMusic("RasaSayang");
    }
}

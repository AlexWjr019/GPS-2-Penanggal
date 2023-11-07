using UnityEngine;

public class ScriptedEvent_6 : MonoBehaviour
{
    public AudioSource windowShatter;
    public GameObject bedroomKey;

    public void PlayWindowShatter()
    {
        Debug.Log("Play window shatter");
        windowShatter.Play();
        bedroomKey.SetActive(true);
    }
}

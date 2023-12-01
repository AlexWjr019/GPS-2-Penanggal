using UnityEngine;

public class ScriptedEvent_6 : MonoBehaviour
{
    public AudioSource windowShatter;
    public GameObject bedroomKey;
    public GameObject cursedPaper;

    public void PlayWindowShatter()
    {
        Debug.Log("Play window shatter");
        windowShatter.Play();
        if(cursedPaper == null)
        {
            bedroomKey.SetActive(true);
        }
    }
}

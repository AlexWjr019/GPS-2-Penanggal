using UnityEngine;

public class CurtainEvent : MonoBehaviour
{
    private AudioSource curtainAudio;

    private void Awake()
    {
        curtainAudio = GetComponent<AudioSource>();
    }

    public void PlayCurtainAudio()
    {
        curtainAudio.Play();
    }
}

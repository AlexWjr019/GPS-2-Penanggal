using UnityEngine;

public class PenanggalKillPlayer : MonoBehaviour
{
    public Camera aiCamera;
    public Camera playerCamera;
    public Animator playerAnimator;
    public LoseScene loseScene;
    public ParticleSystem bloodParticleSystem;

    public void KillPlayer()
    {
        aiCamera.enabled = false;
        playerCamera.enabled = true;
        playerAnimator.enabled = true;
        playerAnimator.SetBool("Dead", true);
    }

    public void PlayBloodParticle()
    {
        bloodParticleSystem.Play();
    }
}

using UnityEngine;

public class PenanggalKillPlayer : MonoBehaviour
{
    public Camera aiCamera;
    public Camera playerCamera;
    public Animator playerAnimator;
    public LoseScene loseScene;
    public ParticleSystem bloodParticleSystem;
    private bool hasKilledPlayer = false;
    public void KillPlayer()
    {
        if (hasKilledPlayer)
        {
            return;
        }

        Debug.Log("KillPlayer called");

        aiCamera.enabled = false;
        playerCamera.enabled = true;
        playerAnimator.enabled = true;
        playerAnimator.SetBool("Dead", true);
        Debug.Log("aiCamera enabled: " + aiCamera.enabled);
        Debug.Log("playerCamera enabled: " + playerCamera.enabled);

        hasKilledPlayer = true;
    }

    public void PlayBloodParticle()
    {
        Debug.Log("PlayBloodParticle called");
        bloodParticleSystem.Play();
    }

    public void ResetKillPlayer()
    {
        hasKilledPlayer = false;
    }
}

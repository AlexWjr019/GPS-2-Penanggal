using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyKillPlayer : MonoBehaviour
{
    public Camera aiCamera;
    public Camera playerCamera;
    public Animator playerAnimator;
    public LoseScene loseScene;
    private bool hasKilledPlayer = false;
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
        GameObject babyPenanggalObj = GameObject.Find("BabyPenanggal");
        if (babyPenanggalObj != null)
        {
            aiCamera = babyPenanggalObj.GetComponentInChildren<Camera>();
        }
        else
        {
            Debug.LogError("BabyPenanggal object not found in the scene!");
        }

        GameObject player = GameObject.Find("Player");
        if (player != null)
        {
            playerAnimator = player.GetComponent<Animator>();
            if (playerAnimator == null)
            {
                Debug.LogError("Animator component not found on Player!");
            }

            Transform mainCameraTransform = player.transform.Find("Head/Main Camera");
            if (mainCameraTransform != null)
            {
                playerCamera = mainCameraTransform.GetComponent<Camera>();
            }
            else
            {
                Debug.LogError("Main Camera not found under Player/Head!");
            }
        }
        else
        {
            Debug.LogError("Player object not found in the scene!");
        }
        loseScene = FindObjectOfType<LoseScene>();
    }
    public void KillPlayer()
    {

        if (hasKilledPlayer)
        {
            return;
        }

        Debug.Log("KillPlayer called");

       // aiCamera.enabled = false;
        playerCamera.enabled = true;
        playerAnimator.enabled = true;
        aiCamera.enabled = false;
        animator.SetBool("isAttacking", false);
        playerAnimator.SetBool("Dead", true);
        Debug.Log("aiCamera enabled: " + aiCamera.enabled);
        Debug.Log("playerCamera enabled: " + playerCamera.enabled);

        hasKilledPlayer = true;
    }

    public void ResetKillPlayer()
    {
        hasKilledPlayer = false;
        GameObject babyPenanggalObj = GameObject.Find("BabyPenanggal");
        if (babyPenanggalObj != null)
        {
            Transform parentObj = babyPenanggalObj.transform.parent;
            if (parentObj != null)
            {
                Destroy(parentObj.gameObject);
            }
            else
            {
                Debug.LogError("BabyPenanggal's parent object not found in the scene!");
            }
        }
        else
        {
            Debug.LogError("BabyPenanggal object not found in the scene!");
        }
    }
}

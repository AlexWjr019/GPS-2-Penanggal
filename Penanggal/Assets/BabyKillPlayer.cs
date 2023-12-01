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
    public Animator animator;
    private void Awake()
    {
        //GameObject babyPenanggalObj = GameObject.Find("BabyPenanggal");
        //if (babyPenanggalObj != null)
        //{
        //    //aiCamera = babyPenanggalObj.GetComponentInChildren<Camera>();
        //    //animator = babyPenanggalObj.GetComponent<Animator>();
        //}
        //else
        //{
        //    Debug.LogError("BabyPenanggal object not found in the scene!");
        //}

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
        playerAnimator.SetBool("Dead", true);
        //animator.SetBool("isAttacking", false);
        Debug.Log("aiCamera enabled: " + aiCamera.enabled);
        Debug.Log("playerCamera enabled: " + playerCamera.enabled);

        hasKilledPlayer = true;
    }

    public void ResetKillPlayer()
    {
        hasKilledPlayer = false;
    }
}

using UnityEngine;

public class EndingCutscene : MonoBehaviour
{
    public Animator playerAnimator;
    public GameObject penanggal;
    public GameObject husband;
    public GameObject lastCursedPaper;

    public EndingAICutscene endingAICutscene;

    public LevelChanger levelChanger;
    public FirstPersonController firstPersonController;
    private HeadBob headBob;

    public AudioSource endingAudio;
    public AudioSource heavyBreathing;
    public AudioSource doorUnlock;

    private bool activated = false;

    private void Awake()
    {
        //Cursor.lockState = CursorLockMode.None;
        headBob = GetComponent<HeadBob>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Ending"))
        {
            if (!activated && lastCursedPaper == null)
            {
                headBob.enabled = false;
                firstPersonController.moveSpeed = 0;
                firstPersonController.enabled = false;
                playerAnimator.enabled = true;
                playerAnimator.SetBool("Ending", true);
                activated = true;
            }
        }
    }

    public void SpawnPenanggal()
    {
        penanggal.SetActive(true);
    }

    public void SpawnHusband()
    {
        husband.SetActive(true);
    }

    public void PlayDoorBanging()
    {
        endingAudio.Play();
        heavyBreathing.Play();
    }

    public void AIGoingBack()
    {
        endingAICutscene.goingBack = true;
    }

    public void PlayDoorUnlock()
    {
        doorUnlock.Play();
    }

    public void GameEnds()
    {
        levelChanger.FadeToNextLevel();
    }
}

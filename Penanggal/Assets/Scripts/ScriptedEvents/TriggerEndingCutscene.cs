using UnityEngine;

public class TriggerEndingCutscene : MonoBehaviour
{
    public GameObject lastCursedPaper;
    public Animator playerAnimator;
    private bool isActivated = false;

    void Update()
    {
        if(isActivated && lastCursedPaper == null)
        {
            playerAnimator.enabled = true;
            playerAnimator.SetBool("Ending", true);
            isActivated = false;
        }
    }
}

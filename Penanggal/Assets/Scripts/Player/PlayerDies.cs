using UnityEngine;

public class PlayerDies : MonoBehaviour
{
    public GameObject loseSceneCanvas;
    public Animator playerAnimator;
    //public Enemy enemy;

    public void PlayerGetsKilled()
    {
        //enemy.playerDied = false;
        loseSceneCanvas.SetActive(true);
        Time.timeScale = 0.0f;
        //enemy.playerDied = true;
        playerAnimator.SetBool("Dead", false);
    }

    public void TurnOffAnimator()
    {
        //playerAnimator.enabled = false;
    }
}

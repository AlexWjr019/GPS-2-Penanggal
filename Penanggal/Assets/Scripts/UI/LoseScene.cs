using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoseScene : MonoBehaviour
{
    public Button Restart;
    public Button mainMenu;
    public GameObject loseUI;

    public Penanggal penanggal;

    private void Awake()
    {
        loseUI.SetActive(false);
    }
    void Start()
    {
        //FirstPersonController
        //fps = FindObjectOfType<FirstPersonController>();
        Restart.onClick.AddListener(RestartGame);
        mainMenu.onClick.AddListener(GoToMainMenu);
    }

    void Update()
    {

    }

    public void RestartGame()
    {
        GameObject player = GameObject.FindWithTag("Player");
        GameObject Ghost = GameObject.FindWithTag("Ghost");
        if (player != null && Ghost != null)
        {
            PositionManager.Instance.ResetPlayerToStartPosition(player);
            Time.timeScale = 1.0f;
            FirstPersonController.hasCollidedWithGhost = false;
            loseUI.SetActive(false);

            penanggal.animator.SetBool("isAttacking", false);
            penanggal.blood.Stop();
        }
        else
        {
            Debug.LogError("Ghost Missing");
            Debug.LogError("Player object not found!");
        }
    }


    public void GoToMainMenu()
    {
        Debug.Log("Trying to load main menu");
        SceneManager.LoadScene("_MainMenu");
        Time.timeScale = 1.0f;
    }

    public void PlayerCollidedWithGhost()
    {
        Time.timeScale = 0.0f;
        loseUI.SetActive(true);
    }

}

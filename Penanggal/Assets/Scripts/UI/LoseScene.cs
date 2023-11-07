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

    private void Awake()
    {
        loseUI.SetActive(false);
    }
    void Start()
    {
        Restart.onClick.AddListener(RestartGame);
        mainMenu.onClick.AddListener(GoToMainMenu);
    }

    void Update()
    {

    }

    public void RestartGame()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            PositionManager.Instance.ResetPlayerToStartPosition(player);
            Time.timeScale = 1.0f;
            loseUI.SetActive(false);
        }
        else
        {
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

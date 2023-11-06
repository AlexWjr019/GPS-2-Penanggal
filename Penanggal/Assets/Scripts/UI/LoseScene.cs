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
    public GameObject Player;

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
        PositionManager.Instance.ResetPlayerToStartPosition(Player);
        Time.timeScale = 1.0f;
        loseUI.SetActive(false);
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

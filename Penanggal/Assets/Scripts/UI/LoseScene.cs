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
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //SceneManager.LoadScene("_MainLevel");
        Time.timeScale = 1.0f;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public Button Button;

    //public GameObject PauseMenuUI;

    void Start()
    {
        Button.onClick.AddListener(Pause);
    }
    //public void PauseGame()
    //{
    //    if(GameIsPaused)
    //    {
    //        Resume();
    //    }
    //    else
    //    {
    //        Pause();
    //    }
    //}

    public void Resume()
    {
        //PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        Debug.Log("Resume");
        Debug.Log(GameIsPaused);
    }

    public void Pause()
    {
        //PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        Debug.Log("Pause");
        Debug.Log(GameIsPaused);
    }
}

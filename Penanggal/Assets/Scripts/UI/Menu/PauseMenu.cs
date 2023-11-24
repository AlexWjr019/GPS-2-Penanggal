using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseUI;
    public Button Button;

    void Start()
    {
        Button.onClick.AddListener(Pause);
        pauseUI.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!GameIsPaused)
            {
                Pause();
            }
            else
            {
                
                Resume();
            }
        }
    }
    public void Resume()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;
        Debug.Log("Resume");
        Debug.Log(GameIsPaused);
        pauseUI.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        GameIsPaused = true;
        Debug.Log("Pause");
        Debug.Log(GameIsPaused);
        pauseUI.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}

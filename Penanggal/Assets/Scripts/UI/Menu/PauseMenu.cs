using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public Button Button;

    void Start()
    {
        Button.onClick.AddListener(Pause);
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;
        Debug.Log("Resume");
        Debug.Log(GameIsPaused);
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        GameIsPaused = true;
        Debug.Log("Pause");
        Debug.Log(GameIsPaused);
    }
}

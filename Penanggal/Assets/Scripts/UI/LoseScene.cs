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
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void Update()
    {

    }

    public void RestartGame()
    {
        GameObject player = GameObject.FindWithTag("Player");
        GameObject ghost = GameObject.FindWithTag("Ghost");

        if (player != null && ghost != null)
        {
            PositionManager.Instance.ResetPlayerToStartPosition(player);

            Time.timeScale = 1.0f;
            loseUI.SetActive(false);

            penanggal.StopAnim();

            FirstPersonController playerController = player.GetComponent<FirstPersonController>();
            if (playerController != null)
            {
                playerController.ResetPlayerState();
            }
        }
        else
        {
            if (player == null)
            {
                Debug.LogError("Player object not found!");
            }
            if (ghost == null)
            {
                Debug.LogError("Ghost Missing");
            }
        }
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }



    public void GoToMainMenu()
    {
        Debug.Log("Trying to load main menu");
        SceneManager.LoadScene("_MainMenu");
        Time.timeScale = 1.0f;
    }

    public void PlayerCollidedWithGhost()
    {
        //Time.timeScale = 0.0f;
        //loseUI.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

}

using UnityEngine;

public class PositionManager : MonoBehaviour
{
    public static PositionManager Instance { get; private set; }
    private Vector3 playerStartPosition;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetPlayerStartPosition(Vector3 position)
    {
        playerStartPosition = position;
        Debug.Log("Player start position set to: " + playerStartPosition);
    }

    public void ResetPlayerToStartPosition(GameObject player)
    {
        CharacterController controller = player.GetComponent<CharacterController>();
        if (controller != null)
        {
            controller.enabled = false;
            player.transform.position = playerStartPosition;
            controller.enabled = true;
            Debug.Log("Player position set to: " + player.transform.position);
        }
    }
}

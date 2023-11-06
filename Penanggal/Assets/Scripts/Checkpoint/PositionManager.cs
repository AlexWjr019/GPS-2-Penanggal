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
        Debug.Log("Player start position set to: " + position);
    }

    public void ResetPlayerToStartPosition(GameObject player)
    {
        player.transform.position = playerStartPosition;
        Debug.Log("Player position reset to start position: " + playerStartPosition);
    }
}

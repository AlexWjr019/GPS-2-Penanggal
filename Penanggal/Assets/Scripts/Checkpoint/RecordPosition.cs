using UnityEngine;

public class RecordPosition : MonoBehaviour
{
    [SerializeField]
    private Vector3 customRespawnPosition;

    private void OnDestroy()
    {
        PositionRecord();
    }

    private void PositionRecord()
    {
        PositionManager.Instance.SetPlayerStartPosition(customRespawnPosition);
    }
}
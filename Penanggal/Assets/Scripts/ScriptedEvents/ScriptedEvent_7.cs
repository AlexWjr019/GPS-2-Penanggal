using UnityEngine;

public class ScriptedEvent_7 : MonoBehaviour
{
    public Transform player;
    public Transform targetLocation;

    private void OnTriggerEnter(Collider other)
    {
        player.transform.position = targetLocation.position;
    }
}

using UnityEngine;

public class TurnOnCeiling : MonoBehaviour
{
    public GameObject ceiling;
    void Start()
    {
        ceiling.SetActive(true);
    }
}
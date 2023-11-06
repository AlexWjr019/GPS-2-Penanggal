using UnityEngine;

public class ScriptedEvent_4 : MonoBehaviour
{
    public GameObject tvScreen;
    public GameObject tvStatic;

    public void PlayRasaSayang()
    {
        tvScreen.SetActive(false);
        tvStatic.SetActive(true);
    }
}

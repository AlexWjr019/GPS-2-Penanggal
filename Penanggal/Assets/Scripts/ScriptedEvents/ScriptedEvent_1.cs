using UnityEngine;

public class ScriptedEvent_1 : MonoBehaviour
{
    public Light[] lights;
    public GameObject[] lightTubes;

    public AudioSource lightbulbBreaking;

    public bool isTriggered = false;

    public void event1()
    {
        if (!isTriggered)
        {
            for (int i = 0; i <= lights.Length - 1; i++)
            {
                lights[i].enabled = false;
                lightTubes[i].SetActive(false);
            }

            lightbulbBreaking.Play();
            isTriggered = true;
        }
     }   
}

using UnityEngine;

public class ScriptedEvent_1 : MonoBehaviour
{
    public Light[] lights;

    /*public bool testingBool = false;

    void Update()
    {
        if(testingBool)
        {
            for (int i = 0; i <= lights.Length - 1; i++)
            {
                lights[i].enabled = false;
            }

        }
    }*/

    public bool isTriggered = false;

    //private void OnTriggerEnter(Collider other)
    //{
    public void event1()
    {
        if (!isTriggered)
        {
            for (int i = 0; i <= lights.Length - 1; i++)
            {
                lights[i].enabled = false;
            }

            FindObjectOfType<AudioManager>().PlaySFX("LightBulbBreaking");
        }
    }   
    //}
}

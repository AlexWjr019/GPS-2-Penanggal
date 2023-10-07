using System.Collections;
using UnityEngine;

public class LightsOut : MonoBehaviour
{
    public Light[] lights;
    public float timeDelay = 5.0f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine("OffLights");
        }
    }

    IEnumerator OffLights()
    {
        yield return new WaitForSeconds(timeDelay);

        for (int pos = 0; pos < 9; pos++)
        {
            lights[pos].enabled = false;
        }
    }
}

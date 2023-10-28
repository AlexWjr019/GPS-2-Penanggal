using UnityEngine;

public class InteractCandle : MonoBehaviour
{
    public GameObject[] candles;
    public bool[] isOn;
    public Light[] candleLights;
    public ParticleSystem[] particleSystems;

    void Update()
    {
        if (candles[0] && !isOn[0])
        {
            candleLights[0].enabled = false;
            particleSystems[0].Play();
            //cabinetAnimator.SetBool("isOpen", false);
        }
        else
        {
            candleLights[0].enabled = true;
            particleSystems[0].Stop();
            //cabinetAnimator.SetBool("isOpen", true);
        }
    }
}

using UnityEngine;

public class SkyManager : MonoBehaviour
{
    /*public float skySpeed;*/

    public int target = 60;

    void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = target;
    }

    /*void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * skySpeed);
    }*/


}

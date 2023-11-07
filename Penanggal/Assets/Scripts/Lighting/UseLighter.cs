using UnityEngine;

public class UseLighter : MonoBehaviour
{
    public Light lighterLight;

    public bool isOn;

    void Update()
    {
        if(isOn)
        {
            lighterLight.enabled = true;
        }
        else
        {
            lighterLight.enabled = false;
        }
    }
}

using UnityEngine;

public class ScriptedEvent_7 : MonoBehaviour
{
    public Light whiteLight;
    public Light redLight;
    public Light spotLight;

    public void PlayScriptedEvent7()
    {
        whiteLight.enabled = false;
        redLight.enabled = true;
        spotLight.enabled = true;
    }
}

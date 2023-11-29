using UnityEngine;
using UnityEngine.UI;

public class DebugText : MonoBehaviour
{
    public Text debugText;
    public Penanggal penanggal;

    private void Update()
    {
        bool isScriptActive = penanggal.enabled;
        debugText.text = "Penanggal script active: " + isScriptActive;
    }
}

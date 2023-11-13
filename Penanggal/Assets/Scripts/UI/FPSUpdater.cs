using UnityEngine;
using TMPro;
public class FPSUpdater : MonoBehaviour
{
    float fps;
    float updateTimer = 0.2f;

    [SerializeField] TextMeshProUGUI fpsTitle;

    private void UpdateFpsDisplay()
    {
        updateTimer -= Time.deltaTime;
        if(updateTimer <= 0.0f)
        {
            fps = 1.0f / Time.unscaledDeltaTime;
            fpsTitle.text = "FPS: " + Mathf.Round(fps);
            updateTimer = 0.2f;
        }
    }

    void Update()
    {
        UpdateFpsDisplay();
    }
}

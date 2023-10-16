using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public TMP_Text moveText;
    public TMP_Text cameratext;
    public TMP_Text hideText;
    public TMP_Text hideCupboardText;
    public Image fillImage;
    private float targetFill = 1f;
    public GameObject player;

    private bool cameraMovementDetected = false;
    private bool fillingComplete = false;
    private bool hideTextAndCoroutineStarted = false;
    private bool showCameraMovingCoroutineStarted = false;

    public static bool cameraMoving;

    // Start is called before the first frame update
    void Start()
    {
        moveText.gameObject.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerMovement.isMove && !showCameraMovingCoroutineStarted)
        {
            moveText.gameObject.SetActive(false);
            StartCoroutine(ShowCameraMoving(3f));
            showCameraMovingCoroutineStarted = true;
        }

        // Check if the player's camera rotation changes
        if (player.transform.rotation != Quaternion.Euler(0f, 0f, 0f))
        {
            cameraMovementDetected = true;
        }

        // Deactivate Cameratext and fillImage if camera movement is detected
        if (cameraMovementDetected)
        {
            cameratext.gameObject.SetActive(false);
            fillImage.gameObject.SetActive(false);
            // Check if the hideText and coroutine have not been started
            if (!hideTextAndCoroutineStarted)
            {
                hideText.gameObject.SetActive(true);
                StartCoroutine(hideTextCount(3f));
                hideTextAndCoroutineStarted = true; // Mark as started
            }
        }

        // Resume the game when filling is complete
        if (fillingComplete)
        {
            //Time.timeScale = 1f;
            return;
        }
    }

    private IEnumerator FillControlPanel(float fillSpeed)
    {
        float currentFill = 0;

        while (currentFill < targetFill)
        {
            currentFill += fillSpeed * Time.unscaledDeltaTime;
            fillImage.fillAmount = Mathf.Clamp01(currentFill);
            yield return null;
        }

        // Mark filling as complete
        fillingComplete = true;
        cameraMoving = true;
    }

    private IEnumerator hideTextCount(float fillSpeed)
    {
        yield return new WaitForSeconds(fillSpeed);
        hideText.gameObject.SetActive(false);
        hideCupboardText.gameObject.SetActive(true);
    }


    private IEnumerator ShowCameraMoving(float fillSpeed)
    {
        yield return new WaitForSeconds(fillSpeed);
        // Pause the game
        //Time.timeScale = 0f;
        cameratext.gameObject.SetActive(true);
        fillImage.gameObject.SetActive(true);

        // Start filling the control panel
        StartCoroutine(FillControlPanel(0.5f));
    }
}

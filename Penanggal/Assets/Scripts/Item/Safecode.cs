using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Safecode : MonoBehaviour
{
    string Code = "123";
    string Num = null;
    int NumIndex = 0;
    public TextMeshProUGUI UiText = null;
    private bool buttonInteractable = true;
    public GameObject safeCodeCanvas;

    //text fade
    public float blinkDuration = 0.2f; // Duration of each blink cycle in seconds.
    public float minAlpha = 0.0f; // Minimum alpha (0 = fully transparent).
    public float maxAlpha = 1.0f; // Maximum alpha (1 = fully opaque).
    private bool isFadingIn = true;
    private bool isBlinking = false;
    private int blinkCounter = 0;

    public void CodeFunction(string Numbers)
    {
        if(buttonInteractable == true)
        {
            NumIndex++;
            Num = Num + Numbers;
            UiText.text = Num;
        }

    }

    public void Enter()
    {
        if(Num == Code)
        {
            buttonInteractable = false;
            UiText.color = Color.green;
            UiText.text = "Successful";
            Debug.Log("It's working");
            StartCoroutine(UnlockSafe());

        }
        else if (Num != Code)
        {
            buttonInteractable = false;
            UiText.color = Color.red;
            UiText.text = "Wrong Password!";
            Debug.Log("Wrong Password! Enter Again!");
            // Start the blinking process.
            StartBlinking(); 
            StartCoroutine(textReset());

        }
    }

    public void Delete()
    {
        if (buttonInteractable == true)
        {
            NumIndex++;
            Num = null;
            UiText.text = Num;
        }

    }

    private IEnumerator UnlockSafe()
    {
        yield return new WaitForSeconds(3f);
        safeCodeCanvas.SetActive(false);
    }

    private IEnumerator textReset()
    {
        yield return new WaitForSeconds(3f);
        UiText.color = Color.black;
        UiText.text = "Safecode...";
        Num = null;
        buttonInteractable = true;
    }

    private void StartBlinking()
    {
        if (!isBlinking)
        {
            isBlinking = true;
            blinkCounter = 0; // Reset the blink counter.
            StartCoroutine(BlinkText());
        }
    }
    private IEnumerator BlinkText()
    {
        while (isBlinking && blinkCounter < 3) // Limit to 3 fading cycles.
        {
            float elapsedTime = 0f;
            float currentAlpha = isFadingIn ? minAlpha : maxAlpha;
            float targetAlpha = isFadingIn ? maxAlpha : minAlpha;

            while (elapsedTime < blinkDuration)
            {
                // Calculate the new alpha value based on the lerp factor.
                float lerpFactor = elapsedTime / blinkDuration;
                float lerpedAlpha = Mathf.Lerp(currentAlpha, targetAlpha, lerpFactor);

                // Apply the new alpha value to the TextMeshPro text.
                Color textColor = UiText.color;
                textColor.a = lerpedAlpha;
                UiText.color = textColor;

                // Increment the elapsed time.
                elapsedTime += Time.deltaTime;

                yield return null;
            }

            // Ensure the text reaches the target alpha exactly.
            Color finalColor = UiText.color;
            finalColor.a = targetAlpha;
            UiText.color = finalColor;

            // Toggle the fade direction (in to out or out to in).
            isFadingIn = !isFadingIn;

            // Wait for a short delay before starting the next blink cycle.
            yield return new WaitForSeconds(0.1f); // Adjust the delay as needed.

            blinkCounter++; // Increment the blink counter.
        }

        // Stop the blinking process and set alpha to 1.
        isBlinking = false;
        Color resetColor = UiText.color;
        resetColor.a = 1.0f;
        UiText.color = resetColor;
    }
}

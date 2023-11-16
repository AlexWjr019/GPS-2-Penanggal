using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveManager : MonoBehaviour
{
    public struct Objective
    {
        public string description;
        public bool isCompleted;
        public GameObject associatedObject; // You can use this to reference the specific object related to the objective
    }

    public Objective[] objectives;
    private int currentObjectiveIndex = 0;

    public TextMeshProUGUI objectiveText;
    public CanvasGroup textCanvasGroup;

    // Add any other necessary variables or methods

    void Start()
    {
        // Initialize objectives
        InitializeObjectives();
        UpdateObjectiveText();
    }

    void InitializeObjectives()
    {
        objectives = new Objective[16];

        // Initialize objectives with their descriptions
        objectives[0].description = "Find your sister";
        objectives[1].description = "Find the bedroom key";
        objectives[2].description = "Investigate the bedroom";
        objectives[3].description = "Unlock the safe";
        objectives[4].description = "Burn the cursed paper";
        objectives[5].description = "Find the nursery room key";
        objectives[6].description = "Investigate the nursery room";
        objectives[7].description = "Rearrange the drawing until it’s complete";
        objectives[8].description = "Hang up the drawing";
        objectives[9].description = "Burn the cursed paper";
        objectives[10].description = "Investigate the mysterious area";
        objectives[11].description = "Investigate the items";
        objectives[12].description = "Rearrange the item in order";
        objectives[13].description = "Burn the curse paper";
        objectives[14].description = "leave the wedding alter area";
        objectives[15].description = "Leave the house";

        // Set the first objective as active
        objectives[currentObjectiveIndex].isCompleted = false;
        // You can also set the associatedObject here if needed
    }

    //void Update()
    //{
    //    // Check if the current objective is completed
    //    if (objectives[currentObjectiveIndex].isCompleted)
    //    {
    //        // Trigger fade out animation or any other transition effect here

    //        // Move to the next objective
    //        currentObjectiveIndex++;

    //        // Check if there are more objectives
    //        if (currentObjectiveIndex < objectives.Length)
    //        {
    //            // Set the new objective as active
    //            objectives[currentObjectiveIndex].isCompleted = false;
    //            // You can also set the associatedObject here if needed

    //            // Trigger fade in animation or any other transition effect here
    //        }
    //        else
    //        {
    //            // All objectives completed, you can implement the end game logic here
    //        }
    //    }
    //}

    void Update()
    {
        if (objectives[currentObjectiveIndex].isCompleted)
        {
            StartCoroutine(TransitionToNextObjective());
        }
    }

    IEnumerator TransitionToNextObjective()
    {
        yield return new WaitForSeconds(5f); // Wait for 5 seconds

        // Trigger fade out animation or any other transition effect here
        StartCoroutine(FadeCanvasGroup(textCanvasGroup, 0f, 1f, 1.0f));

        currentObjectiveIndex++;

        if (currentObjectiveIndex < objectives.Length)
        {
            // Set the new objective as active
            objectives[currentObjectiveIndex].isCompleted = false;
            UpdateObjectiveText();

            // Trigger fade in animation or any other transition effect here
            StartCoroutine(FadeCanvasGroup(textCanvasGroup, 1f, 0f, 5.0f));
        }
        else
        {
            // All objectives completed, you can implement the end game logic here
        }
    }

    void UpdateObjectiveText()
    {
        if (objectiveText != null)
        {
            objectiveText.text = objectives[currentObjectiveIndex].description;
        }
    }

    IEnumerator FadeCanvasGroup(CanvasGroup canvasGroup, float startAlpha, float targetAlpha, float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / duration);
            canvasGroup.alpha = alpha;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Set the final alpha
        canvasGroup.alpha = targetAlpha;
    }
}

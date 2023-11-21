using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveManager2 : MonoBehaviour
{
    public struct Objective
    {
        public string description;
        public bool isCompleted;
    }

    public Objective[] objectives;
    private int currentObjectiveIndex = 0;

    public TextMeshProUGUI objectiveText;

    public static bool objective;
    public Animation objectiveFade;

    void Start()
    {
        objective = false;
        // Initialize objectives
        InitializeObjectives();
        UpdateObjectiveText();
    }

    void InitializeObjectives()
    {
        objectives = new Objective[16];

        // Initialize objectives with their descriptions
        objectives[0].description = "Investigate the items";
        objectives[1].description = "Rearrange the item in order";
        objectives[2].description = "Burn the curse paper";
        objectives[3].description = "leave the wedding alter area";
        objectives[4].description = "Leave the house";

        // Set the first objective as active
        objectives[currentObjectiveIndex].isCompleted = false;

    }

    void Update()
    {
        CheckObjective();

        // Check if the current objective is completed
        if (objectives[currentObjectiveIndex].isCompleted)
        {
            // Trigger fade out animation or any other transition effect here
            objectiveFade.Play("ObjectiveFadeOut");
            // Move to the next objective
            currentObjectiveIndex++;

            // Check if there are more objectives
            if (currentObjectiveIndex < objectives.Length)
            {
                // Set the new objective as active
                objectives[currentObjectiveIndex].isCompleted = false;
                UpdateObjectiveText();

                // Trigger fade in animation or any other transition effect here
                objectiveFade.Play("ObjectiveFadeIn");
            }
            else
            {

            }
        }
    }

    void UpdateObjectiveText()
    {
        if (objectiveText != null)
        {
            objectiveText.text = objectives[currentObjectiveIndex].description;
        }
    }

    public void CheckObjective()
    {
        if (objective)
        {
            objectives[currentObjectiveIndex].isCompleted = true;
            Debug.Log("objective 1 complete");
            objective = false;
        }

    }
}

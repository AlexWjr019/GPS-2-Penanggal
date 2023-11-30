using System;
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
    }

    public Objective[] objectives;
    private int currentObjectiveIndex = 0;

    public TextMeshProUGUI objectiveText;

    public static bool objective;
    public Animation objectiveFade;

    public GameObject objText;
    private bool objTextActive = true;

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
        objectives[0].description = "Find your sister";
        objectives[1].description = "Burn the cursed paper";
        objectives[2].description = "Find the bedroom key";
        objectives[3].description = "Investigate the bedroom";
        objectives[4].description = "Unlock the safe";
        objectives[5].description = "Burn the cursed paper";
        objectives[6].description = "Find the nursery room key";
        objectives[7].description = "Investigate the nursery room";
        objectives[8].description = "Rearrange the drawing until it’s complete";
        objectives[9].description = "Hang up the drawing";
        objectives[10].description = "Burn the cursed paper";
        objectives[11].description = "Investigate the mysterious area";

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

    public void HideObjective()
    {
        if (objTextActive)
        {
            objText.SetActive(false);
            objTextActive = false;
        }
        else if (!objTextActive)
        {
            objText.SetActive(true);
            objTextActive = true;
        }
    }
}

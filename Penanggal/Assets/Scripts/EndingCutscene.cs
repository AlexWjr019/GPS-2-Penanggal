using UnityEngine;

public class EndingCutscene : MonoBehaviour
{
    public LevelChanger levelChanger;
    [SerializeField] private GameObject cursePaper;

    private void OnTriggerEnter(Collider other)
    {
        if(cursePaper == null)
        {
            levelChanger.FadeToNextLevel();
        }
    }
}

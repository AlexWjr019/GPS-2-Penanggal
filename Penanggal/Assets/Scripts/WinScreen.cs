using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreen : MonoBehaviour
{
    public FlameObject fo;
    public int sceneID;

    // Start is called before the first frame update
    void Start()
    {
        //fo = FindObjectOfType<FlameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (fo.cursePaperObjects[0] == null && fo.cursePaperObjects[1] == null && fo.cursePaperObjects[2] == null && fo.cursePaperObjects[3] == null)
        {
            Debug.Log("next scene");
            SceneManager.LoadScene(sceneID);
        }
    }
}

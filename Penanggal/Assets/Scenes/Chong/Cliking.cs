using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    private int currentColorIndex = 0;
    private Color[] colors = new Color[5];

    private MeshRenderer meshRenderer;

    private void Start()
    {
        colors[0] = Color.red;
        colors[1] = Color.blue;
        colors[2] = Color.green;
        colors[3] = Color.yellow;
        colors[4] = Color.magenta;

        meshRenderer = GetComponent<MeshRenderer>();
        if (meshRenderer == null)
        {
            Debug.LogError("No detected your colour.");
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.transform == transform)
            {
                ChangeColor();
            }
        }
    }

    private void ChangeColor()
    {
        currentColorIndex = Random.Range(0, colors.Length);
        meshRenderer.material.color = colors[currentColorIndex];
    }
}


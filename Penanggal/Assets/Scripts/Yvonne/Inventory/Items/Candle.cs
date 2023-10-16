using UnityEngine;

public class Candle : MonoBehaviour, IItems
{
    public string Name
    {
        get
        {
            return "Candle";
        }
    }

    public Sprite image = null;

    public Sprite Image
    {
        get
        {
            return image;
        }

    }

    public void OnPickup()
    {
        gameObject.SetActive(false);
    }

    public void OnDrop()
    {
        RaycastHit hit = new RaycastHit();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out hit, 1000))
        {
            gameObject.SetActive(true);
            gameObject.transform.position = hit.point;
        }
    }
}

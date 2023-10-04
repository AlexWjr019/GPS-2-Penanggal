using UnityEngine;

public class InteracionItem : MonoBehaviour, IInteractableItem
{
    public string itemName;
    //public Material itemMaterial;
    public Sprite itemSprite;

    private void Start()
    {
        itemName = gameObject.name;
        //itemMaterial = GetComponent<MeshRenderer>().material;
    }

    public string GetName()
    {
        return itemName;
    }

    //public Material GetMaterial()
    //{
    //    return itemMaterial;
    //}

    // Added method: Getter method to access the itemSprite.
    public Sprite GetSprite()
    {
        return itemSprite;
    }
}

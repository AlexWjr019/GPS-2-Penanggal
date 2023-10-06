using UnityEngine;

public class InteracionItem : MonoBehaviour, IInteractableItem
{
    public string itemName;
    public Sprite itemSprite;
    public GameObject itemPrefab3D;

    private void Start()
    {
        itemName = gameObject.name;
    }

    public string GetName()
    {
        return itemName;
    }

    public Sprite GetSprite()
    {
        return itemSprite;
    }

    // 新的方法，返回3D预制体的引用
    public GameObject GetPrefab3D()
    {
        return itemPrefab3D;
    }
}

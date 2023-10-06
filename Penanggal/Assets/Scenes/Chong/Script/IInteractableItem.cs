using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IInteractableItem
{
    string GetName();
    Sprite GetSprite();
    GameObject GetPrefab3D();
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newItem", menuName = "Items/New Item")]
public class ItemData : ScriptableObject
{
    public string itemName;

    public Sprite itemIcon;

    public GameConstants.EntityID itemID;

    public int maxStackAmount;

    public GameObject handObject;

    //
    public virtual void OnLeftClick()
    {

    }
    
    public virtual void OnRightClick()
    {

    }
}

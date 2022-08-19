using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is responsible for managing the referance to every item in the game.
/// </summary>
public class ItemReferanceManager : MonoBehaviour
{
    private static ItemReferanceManager Instance;

    [SerializeField] private ItemData[] itemArray;

    //
    private void Awake()
    {
        Instance = this;
    }

    public static ItemData GetItem(GameConstants.ItemID _itemID)
    {
        return Instance.itemArray[(int)_itemID];
    }
}

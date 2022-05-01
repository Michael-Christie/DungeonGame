using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public ItemData itemData { get; private set; }

    public int itemAmount { get; private set; }

    [SerializeField] private Image imgItemIcon;

    //
    public void SetUp(ItemData _itemSO, int _amount)
    {
        itemData = _itemSO;
        itemAmount = _amount;

        imgItemIcon.sprite = itemData?.itemIcon;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Inventory
{
    public ItemData itemData;

    public int amount;

    public bool IsFull
    {
        get
        {
            return itemData?.maxStackAmount <= amount;
        }
    } 

    public bool IsEmpty
    {
        get
        {
            return itemData == null;
        }
    }
}

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance { get; private set; }

    public Inventory[] Inventory { get; private set; } = new Inventory[25];

    public Action onInventoryUpdate;
    public Action onHotbarUpdate;

    //
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        for (int i = 0; i < Inventory.Length; i++)
        {
            Inventory[i] = new Inventory();
        }
    }

    private bool tempBool;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!tempBool)
            {
                MenuManager.Instance.ShowMenu((int)GameConstants.Menus.Inventory);
            }
            else
            {
                MenuManager.Instance.HideMenu();
            }
            tempBool = !tempBool;
        }
    }

    public bool AddItem(ItemData _itemData, ref int _amount)
    {
        int _desiredSlot = -1;

        for (int i = 0; i < Inventory.Length; i++)
        {
            //find the first empty slot
            if (Inventory[i].IsEmpty)
            {
                if (_desiredSlot == -1)
                {
                    _desiredSlot = i;
                }
                continue;
            }

            if (Inventory[i].IsFull)
            {
                continue;
            }

            if(Inventory[i].itemData == _itemData)
            {
                //if we can just add the two amounts together, done
                if (Inventory[i].amount + _amount <= Inventory[i].itemData.maxStackAmount)
                {
                    Inventory[i].amount += _amount;

                    onInventoryUpdate?.Invoke();
                    return true;
                }
                //else we need to add till the stack is full, and then keep searching for empty space
                _amount = (Inventory[i].amount + _amount) - Inventory[i].itemData.maxStackAmount;
            }
        }

        //No space in inv
        if (_desiredSlot == -1)
        {
            return false;
        }

        Inventory[_desiredSlot].itemData = _itemData;
        Inventory[_desiredSlot].amount = _amount;

        onInventoryUpdate?.Invoke();

        return true;
    }
}
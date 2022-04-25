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

    [SerializeField] private Inventory[] inventory = new Inventory[25];

    //
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            inventory[i] = new Inventory();
        }
    }

    public bool AddItem(ItemData _itemData, ref int _amount)
    {
        int _desiredSlot = -1;

        for (int i = 0; i < inventory.Length; i++)
        {
            //find the first empty slot
            if (inventory[i].IsEmpty)
            {
                if (_desiredSlot == -1)
                {
                    _desiredSlot = i;
                }
                continue;
            }

            if (inventory[i].IsFull)
            {
                continue;
            }

            if(inventory[i].itemData == _itemData)
            {
                //if we can just add the two amounts together, done
                if (inventory[i].amount + _amount <= inventory[i].itemData.maxStackAmount)
                {
                    inventory[i].amount += _amount;
                    return true;
                }
                //else we need to add till the stack is full, and then keep searching for empty space
                _amount = (inventory[i].amount + _amount) - inventory[i].itemData.maxStackAmount;
            }
        }

        //No space in inv
        if (_desiredSlot == -1)
        {
            return false;
        }

        inventory[_desiredSlot].itemData = _itemData;
        inventory[_desiredSlot].amount = _amount;

        return true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseItem : BaseEntity, IPoolable, IInteractable
{
    public GameConstants.ItemID item;

    public virtual int PoolID => (int)item;

    public bool IsInScene { get; set; }

    public ItemData itemData;

    public int amount = 1;

    //
    public void ReturnToPool()
    {
        gameObject.SetActive(false);
        IsInScene = false;

        gameObject.transform.SetParent(ObjectPooler.Instance.transform);
    }

    public void SetPosition(Transform _newParent)
    {
        SetPosition(_newParent, _newParent.position);
    }

    public void SetPosition(Transform _newParent, Vector3 _position)
    {
        transform.SetParent(_newParent);
        transform.position = _position;

        gameObject.SetActive(true);
    }

    public virtual void OnInteract()
    {
        //Add it to the inventory
        if (PlayerInventory.Instance.AddItem(itemData, ref amount))
        {
            ReturnToPool();
        }
        else
        {
            //inventory is full and cannot take the item;
            Debug.Log("Inv is full");
        }
    }
}

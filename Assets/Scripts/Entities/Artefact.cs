using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MC.Core;

public class Artefact : BaseEntity, IPoolable, IInteractable
{
    public int PoolID { get { return (int)GameConstants.EntityID.Artefact; } }

    public bool IsInScene { get; set; } = false;

    //
    #region IPoolable
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
    #endregion

    #region IInteractable
    public void OnInteract()
    {
        ReturnToPool();
        GameManager.Instance.ArtefactCollected();
    }
    #endregion
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MC.Core;

public class Artefact : BaseItem
{
    #region IInteractable
    public override void OnInteract()
    {
        ReturnToPool();
        GameManager.Instance.ArtefactCollected();
    }
    #endregion
}

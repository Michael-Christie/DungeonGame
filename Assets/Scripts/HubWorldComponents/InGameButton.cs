using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameButton : MonoBehaviour, IInteractable
{

    public void OnInteract()
    {
        HubManager.Instance.TurnOnPortal();
    }
}

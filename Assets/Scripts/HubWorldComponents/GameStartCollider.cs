using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class GameStartCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider _other)
    {
        if(_other.CompareTag(GameConstants.Tags.player))
        {
            HubManager.Instance.StartGameCountDown();
        }
    }

    private void OnTriggerExit(Collider _other)
    {
        if (_other.CompareTag(GameConstants.Tags.player))
        {
            HubManager.Instance.StopGameCountDown();
        }
    }
}

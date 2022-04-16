using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEndCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider _other)
    {
        if (_other.CompareTag(GameConstants.Tags.player))
        {
            GameManager.Instance.StartEndGame();
        }
    }

    private void OnTriggerExit(Collider _other)
    {
        if (_other.CompareTag(GameConstants.Tags.player))
        {
            GameManager.Instance.CancelEndGame();
        }
    }
}

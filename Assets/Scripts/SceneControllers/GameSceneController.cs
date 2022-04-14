using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using DG.Tweening;
using MC.Core;
using MC.DiscordManager;
using System;

public class GameSceneController : BaseSceneLoader
{
    [SerializeField] private GameObject leftDoor;
    [SerializeField] private GameObject rightDoor;

    public override void OnSceneReady()
    {
        GameOverMenu.endGameReason = EndGameReason.Completed;
        MenuManager.Instance.ShowMenu((int)GameConstants.Menus.GameOver);
    }

    public override void OnSceneStart()
    {
        if (MCDiscordManager.Instance.IsInitialized)
        {
            MCDiscordManager.Instance.SetActivity("Exploring A Dungeon", "");
        }
        StartCoroutine(OpenDoors());
    }

    public override void OnSceneChange()
    {
    }

    private IEnumerator OpenDoors()
    {
        yield return GameConstants.WaitTimers.waitForTwoSeconds;

        leftDoor.transform.DOLocalRotate(Vector3.up * -50, GameConstants.Animations.rotateTime);
        rightDoor.transform.DOLocalRotate(Vector3.up * 65, GameConstants.Animations.rotateTime);

        GameManager.Instance.StartGame();
    }
}

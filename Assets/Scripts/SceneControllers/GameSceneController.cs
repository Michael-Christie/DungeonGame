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

    [SerializeField] private MobSpawner[] mobSpawners;

    public override void OnSceneReady()
    {
        for(int i = 0; i < mobSpawners.Length; i++)
        {
            mobSpawners[i].SpawnEnemysInWorld();
        }

        MenuManager.Instance.ShowMenu((int)GameConstants.Menus.GameHud);
        PlayerController.Instance.EnableCharacter();
    }

    public override void OnSceneStart()
    {
        GameManager.Instance.CancelEndGame();

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

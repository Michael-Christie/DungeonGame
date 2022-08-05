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
    }

    public override void OnSceneStart()
    {
        GameManager.Instance.StartGame();
        PlayerController.Instance.EnableCharacter();
    }

    public override void OnSceneChange()
    {
    }
}

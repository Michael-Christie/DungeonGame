using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MC.Core;
using MC.DiscordManager;

public class HubSceneController : BaseSceneLoader
{
    [SerializeField] private PlayerClass[] playerClasses;

    //
    public override void OnSceneReady()
    {
        Debug.Log("Scene Ready");
        PlayerController.Instance.SetPlayerClass(playerClasses[(int)GameData.Instance.GetPlayerData().charType]);
        PlayerController.Instance.EnableCharacter();
    }

    public override void OnSceneStart()
    {
        Debug.Log("Scene Start");
        if (MCDiscordManager.Instance.IsInitialized)
        {
            MCDiscordManager.Instance.SetActivity("In Hub", "");
        }

        HubManager.Instance.StopGameCountDown();
    }

    public override void OnSceneChange()
    {
        Debug.Log("Scene Change");
        PlayerController.Instance.DisableCharacter();
    }
}

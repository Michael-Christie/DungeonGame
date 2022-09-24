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

        MenuManager.Instance.ShowMenu((int)GameConstants.Menus.HubHud);
    }

    public override void OnSceneStart()
    {
        Debug.Log("Scene Start");
        if (MCDiscordManager.Instance.IsInitialized)
        {
            MCDiscordManager.Instance.SetActivity("In Hub", "");
        }

        (MenuManager.Instance.GetMenuAtIndex((int)GameConstants.Menus.Inventory) as InventoryMenu).PopulateInventoryMenu();
    }

    public override void OnSceneChange()
    {
        Debug.Log("Scene Change");
        PlayerController.Instance.DisableCharacter();
    }
}

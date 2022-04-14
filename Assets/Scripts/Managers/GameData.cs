using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public static GameData Instance { get; private set; }

    private PlayerData playerData;

    //
    private void Awake()
    {
        Instance = this;
    }

    public void SetPlayerData(PlayerData _data)
    {
        playerData = _data;
    }

    public PlayerData GetPlayerData()
    {
        return playerData;
    }
}

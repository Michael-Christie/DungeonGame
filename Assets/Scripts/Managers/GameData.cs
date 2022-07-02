using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public static GameData Instance { get; private set; }

    private PlayerData playerData;

    private int saveSlot = -1;

    //
    private void Awake()
    {
        Instance = this;
    }

    private void OnApplicationQuit()
    {
        //Save data...
        if(playerData.Equals(default(PlayerData)) || saveSlot == -1)
        {
            return;
        }

        SaveData();
    }

    public void SetPlayerData(PlayerData _data, int _saveSlot)
    {
        playerData = _data;
        saveSlot = _saveSlot;

        StartCoroutine(SlowUpdate());
    }

    public PlayerData GetPlayerData()
    {
        return playerData;
    }

    private IEnumerator SlowUpdate()
    {
        while (true)
        {
            yield return GameConstants.WaitTimers.waitForOneRealSecond;

            StatsManager.Instance?.AddValueToStat(GameConstants.Stats.PlayTime, 1);
        }
    }

    public void SaveData()
    {
        playerData.stats = StatsManager.Instance.Stats;

        SaveFileHelper.SaveDataXML(GameConstants.GetSavePathFromInt(saveSlot), playerData);
    }
}

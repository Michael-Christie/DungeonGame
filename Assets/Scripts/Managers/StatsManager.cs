using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public partial class StatsManager : MonoBehaviour
{
    public static StatsManager Instance { get; private set; }

    //
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        LoadStats();
    }

    public void LoadStats()
    {
        //pull the stat data from somewhere.
        StatData[] _rawData = GameData.Instance.GetPlayerData().stats;

        if (_rawData == null)
        {
            return;
        }

        for(int i = 0; i < Stats.Length; i++)
        {
            for(int j = 0; j < _rawData.Length; j++)
            {
                if(Stats[i].statID == _rawData[j].statID)
                {
                    Stats[i].value = _rawData[j].value;
                    break;
                }
            }
        }

        //After I've loaded data, maybe I should order it in (int)Stats order?
        Stats = Stats.OrderBy(x => (int)x.statID).ToArray();
    }

    public void AddValueToStat(GameConstants.Stats _stat, float _value)
    {
        foreach(StatData _data in Stats)
        {
            if(_data.statID != _stat)
            {
                continue;
            }

            _data.value += _value;
            return;
        }

        Debug.Log($"Could not find Stat {_stat} to add value {_value}");
    }

    public void SetValueToStat(GameConstants.Stats _stat, float _value)
    {
        foreach (StatData _data in Stats)
        {
            if (_data.statID != _stat)
            {
                continue;
            }

            _data.value = _value;
            return;
        }

        Debug.Log($"Could not find Stat {_stat} to set value {_value}");
    }

    public void SetMaxValueToStat(GameConstants.Stats _stat, float _value)
    {
        foreach (StatData _data in Stats)
        {
            if (_data.statID != _stat)
            {
                continue;
            }

            _data.value = Mathf.Max(_value, _data.value);
            return;
        }

        Debug.Log($"Could not find Stat {_stat} to set max value {_value}");
    }

    /// <summary>
    /// Gets the value of a stat
    /// </summary>
    /// <param name="_stat"></param>
    /// <returns>Returns stat value or -1 if stat not found.</returns>
    public float GetValue(GameConstants.Stats _stat)
    {
        foreach (StatData _data in Stats)
        {
            if (_data.statID != _stat)
            {
                continue;
            }

            return _data.value;

        }

        return -1;
    }
}

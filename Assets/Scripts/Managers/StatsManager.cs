using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using MC.Core;

public partial class StatsManager : BaseValueTracker
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
            for (int i = 0; i < Stats.Length; i++)
            {
                for (int j = 0; j < _rawData.Length; j++)
                {
                    if (Stats[i].statID == _rawData[j].statID)
                    {
                        Stats[i].Value = _rawData[j].Value;
                        break;
                    }
                }
            }
        }

        //reorder the array to be ordered by stat ID
        Stats = Stats.OrderBy(x => (int)x.statID).ToArray();
    }

    public void AddValueToStat(GameConstants.Stats _stat, float _value)
    {
        AddValueToStat((int)_stat, _value);
    }

    public void SetValueToStat(GameConstants.Stats _stat, float _value)
    {
        SetValueToStat((int)_stat, _value);
    }

    public void SetMaxValueToStat(GameConstants.Stats _stat, float _value)
    {
        SetMaxValueToStat((int)_stat, _value);
    }

    /// <summary>
    /// Gets the value of a stat
    /// </summary>
    /// <param name="_stat"></param>
    /// <returns>Returns stat value or -1 if stat not found.</returns>
    public float GetValue(GameConstants.Stats _stat)
    {
        return GetValue((int)_stat);
    }
}

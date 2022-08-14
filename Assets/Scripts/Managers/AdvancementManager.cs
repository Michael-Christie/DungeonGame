using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class AdvancementManager : BaseValueTracker
{
    public static AdvancementManager Instance { get; private set; }

    // 
    private void Awake()
    {
        Instance = this;
    }

    public AdvancementData GetAdvancement(int _index)
    {
        if(_index >= 0 && _index < Advancement.Length)
        {
            return Advancement[_index];
        }

        return null;
    }

    public void ClaimReward(int _index)
    {

    }
}

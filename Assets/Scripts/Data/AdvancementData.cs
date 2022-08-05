using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class AdvancementManager : BaseValueTracker
{
    protected override BaseValueData[] data { get { return Advancement; } }

    public AdvancementData[] Advancement { get; private set; } = new AdvancementData[]
    {
        new AdvancementData()
        {
            advancementID = GameConstants.Advancement.Killer,
            displayName = "Killer Man",
            description = "Kill 100 Enemies in on Run",
            reward = new Reward
                {
                    type = GameConstants.RewardType.XP,
                    amount = 100
                }
        }
    };
}

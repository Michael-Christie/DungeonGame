using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class StatsManager : BaseValueTracker
{
    protected override BaseValueData[] data { get { return Stats; } }

    public StatData[] Stats { get; private set; } = new StatData[]
    {
        new StatData()
        {
            statID = GameConstants.Stats.PlayTime,
            category = GameConstants.StatCategory.General,
            DisplayName = "Play Time"
        },
        new StatData()
        {
            statID = GameConstants.Stats.DungeonsPlayed,
            category = GameConstants.StatCategory.Dungeons,
            DisplayName = "Dungeons Played"
        },
        new StatData()
        {
            statID = GameConstants.Stats.DungeonsComplete,
            category = GameConstants.StatCategory.Dungeons,
            DisplayName = "Dungeons Completed"
        },
        new StatData()
        {
            statID = GameConstants.Stats.DungeonsLost,
            category = GameConstants.StatCategory.Dungeons,
            DisplayName = "Dungeons Failed"
        },
        new StatData()
        {
            statID = GameConstants.Stats.DungeonsAbandoned,
            category = GameConstants.StatCategory.Dungeons,
            DisplayName = "Dungeons Abandoned"
        },
        new StatData()
        {
            statID = GameConstants.Stats.EnemiesKilled,
            category = GameConstants.StatCategory.Enemies,
            DisplayName = "Enemies Killed"
        }
    };
}

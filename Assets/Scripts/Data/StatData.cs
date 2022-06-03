using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class StatsManager : MonoBehaviour
{
    public StatData[] Stats { get; private set; } = new StatData[]
    {
        new StatData()
        {
            statID = GameConstants.Stats.PlayTime,
            category = GameConstants.StatCategory.General,
            displayName = "Play Time"
        },
        new StatData()
        {
            statID = GameConstants.Stats.DungeonsPlayed,
            category = GameConstants.StatCategory.Dungeons,
            displayName = "Dungeons Played"
        },
        new StatData()
        {
            statID = GameConstants.Stats.DungeonsComplete,
            category = GameConstants.StatCategory.Dungeons,
            displayName = "Dungeons Completed"
        },
        new StatData()
        {
            statID = GameConstants.Stats.DungeonsLost,
            category = GameConstants.StatCategory.Dungeons,
            displayName = "Dungeons Failed"
        },
        new StatData()
        {
            statID = GameConstants.Stats.DungeonsAbandoned,
            category = GameConstants.StatCategory.Dungeons,
            displayName = "Dungeons Abandoned"
        },
        new StatData()
        {
            statID = GameConstants.Stats.EnemiesKilled,
            category = GameConstants.StatCategory.Enemies,
            displayName = "Enemies Killed"
        }
    };
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class StatsMenu : MenuBase
{
    //Tempoarily just do this, and come up with a better solution when this is more imporant
    [SerializeField] private TextMeshProUGUI txtPlayTime; 
    [SerializeField] private TextMeshProUGUI txtDungeonsPlayed; 
    [SerializeField] private TextMeshProUGUI txtDungeonsWon; 
    [SerializeField] private TextMeshProUGUI txtDungeonsLost; 
    [SerializeField] private TextMeshProUGUI txtDungeonsAbandoded; 
    [SerializeField] private TextMeshProUGUI txtEnemiesKilled; 


    protected override void Initalize()
    {
    }

    public override void Show(Action _onShowComplete)
    {
        txtPlayTime.text = StatsManager.Instance.GetValue(GameConstants.Stats.PlayTime).ToString();
        txtDungeonsPlayed.text = StatsManager.Instance.GetValue(GameConstants.Stats.DungeonsPlayed).ToString("N0");
        txtDungeonsWon.text = StatsManager.Instance.GetValue(GameConstants.Stats.DungeonsComplete).ToString("N0");
        txtDungeonsLost.text = StatsManager.Instance.GetValue(GameConstants.Stats.DungeonsLost).ToString("N0");
        txtDungeonsAbandoded.text = StatsManager.Instance.GetValue(GameConstants.Stats.DungeonsAbandoned).ToString("N0");
        txtEnemiesKilled.text = StatsManager.Instance.GetValue(GameConstants.Stats.EnemiesKilled).ToString("N0");

        base.Show(_onShowComplete);
        OnShowComplete();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AchievementElement : PooledCardBase
{
    [SerializeField] private Button btnClaim;

    [SerializeField] private TextMeshProUGUI txtTitle;
    [SerializeField] private TextMeshProUGUI txtDescription;
    [SerializeField] private TextMeshProUGUI txtProgressAmount;
    [SerializeField] private TextMeshProUGUI txtReward;

    [SerializeField] private Slider progressBar;

    [SerializeField] private Image imgIcon;

    //
    public override void UpdateContent()
    {
        base.UpdateContent();
    }
}

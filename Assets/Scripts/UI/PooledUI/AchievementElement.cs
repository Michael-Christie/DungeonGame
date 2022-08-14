using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using MC.Core;

public class AchievementElement : PooledCardBase
{
    [SerializeField] private Button btnClaim;

    [SerializeField] private TextMeshProUGUI txtTitle;
    [SerializeField] private TextMeshProUGUI txtDescription;
    [SerializeField] private TextMeshProUGUI txtProgressAmount;
    [SerializeField] private TextMeshProUGUI txtReward;

    [SerializeField] private Slider progressBar;

    [SerializeField] private Image imgIcon;

    private AdvancementData cachedAdvancement;

    //
    private void Start()
    {
        btnClaim.onClick.AddListener(ClaimReward);
    }

    public override void UpdateContent()
    {
        cachedAdvancement = AdvancementManager.Instance.GetAdvancement(Index);

        if(cachedAdvancement == null)
        {
            return;
        }

        txtTitle.text = cachedAdvancement.DisplayName;
        txtDescription.text = cachedAdvancement.description;
        txtProgressAmount.text = $"{cachedAdvancement.Value} / {cachedAdvancement.Value}";

        progressBar.maxValue = cachedAdvancement.Value;
        progressBar.value = cachedAdvancement.Value;

        if (cachedAdvancement.reward != null)
        {
            txtReward.text = $"{cachedAdvancement.reward.amount} {cachedAdvancement.reward.type}";
        }
        else
        {
            txtReward.text = string.Empty;
        }

    }

    private void ClaimReward()
    {
        AdvancementManager.Instance.ClaimReward(cachedAdvancement.ValueIndex);
    }
}

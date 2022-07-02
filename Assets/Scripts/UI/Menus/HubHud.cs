using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using DG.Tweening;

public class HubHud : MenuBase
{
    [SerializeField] private TextMeshProUGUI txtCoutdown;

    public static Action<int> ShowCountdownDigit { get; private set; }

    //
    protected override void Initalize()
    {
        ShowCountdownDigit += ShowCoutdown;
    }

    public override void Show(Action _onShowComplete)
    {
        base.Show(_onShowComplete);

        menuPanel.SetActive(true);

        OnShowComplete();
    }

    public override void Hide(Action _onHideComplete)
    {
        base.Hide(_onHideComplete);

        menuPanel.SetActive(false);

        OnHideComplete();
    }

    public override void OnEscHit()
    {
        MenuManager.Instance.ShowMenu((int)GameConstants.Menus.Pause);
    }

    private void ShowCoutdown(int _value)
    {
        txtCoutdown.text = _value.ToString();

        StartCoroutine(AnimateCoutdown());
    }

    private IEnumerator AnimateCoutdown()
    {
        txtCoutdown.transform.DOScale(1, GameConstants.Animations.scaleTimeShort);

        yield return GameConstants.WaitTimers.waitForOneSecond;

        txtCoutdown.transform.DOScale(0, GameConstants.Animations.scaleTimeShort);
    }
}

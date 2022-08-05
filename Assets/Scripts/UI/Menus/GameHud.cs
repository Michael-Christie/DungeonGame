using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;
using TMPro;

public class GameHud : MenuBase
{
    [SerializeField] private TextMeshProUGUI txtTimer;

    [SerializeField] private Slider healthBar;

    private int lastHealth = 12;

    private bool canUpdateHealth = true;

    //
    protected override void Initalize()
    {

    }

    public override void Show(Action _onShowComplete)
    {
        base.Show(_onShowComplete);

        menuPanel.SetActive(true);

        healthBar.maxValue = PlayerController.Instance.playerClass.MaxHealth;
        //This is tempoary and needs to be moved to something else but health isn't important rn
        healthBar.value = PlayerController.Instance.playerClass.MaxHealth;

        GameManager.Instance.onTimerUpdate += UpdateTimer;

        OnShowComplete();
    }

    public override void Hide(Action _onHideComplete)
    {
        base.Hide(_onHideComplete);

        menuPanel.SetActive(false);

        GameManager.Instance.onTimerUpdate -= UpdateTimer;

        OnHideComplete();
    }

    public override void OnEscHit()
    {
        MenuManager.Instance.ShowMenu((int)GameConstants.Menus.Pause);
    }

    /// <summary>
    /// Updates the hearts on the Hud
    /// </summary>
    /// <param name="_currentHealth">A number between 0 and max health (12)</param>
    private bool UpdateHearts(int _currentHealth)
    {
        if (!canUpdateHealth) //This check is to ensure the hearts don't rotate too much
            return false;

        healthBar.DOValue(_currentHealth, GameConstants.Animations.shakeTimeShort);

        //for (int i = 0; i < hearts.Length; i++)
        //{
        //    hearts[i].fillAmount = Mathf.Clamp01(_currentHealth * 0.5f - i);

        //    //Rotate hearts if they have updated (decreased || increased)
        //    if ((i >= Mathf.FloorToInt(_currentHealth * 0.5f)
        //            && i < Mathf.CeilToInt(lastHealth * 0.5f))
        //        || (i >= Mathf.FloorToInt(lastHealth * 0.5f)
        //            && i < Mathf.CeilToInt(_currentHealth * 0.5f)))
        //    {
        //        canUpdateHealth = false;
        //        hearts[i].transform.parent.DOShakeRotation(GameConstants.Animations.shakeTimeShort, Vector3.forward * 90)
        //            .OnComplete(delegate
        //            {
        //                canUpdateHealth = true;
        //            });
        //    }
        //}

        lastHealth = _currentHealth;
        return true;
    }

    public void UpdateTimer(GameTime _time)
    {
        txtTimer.text = _time.ToString();
    }
}

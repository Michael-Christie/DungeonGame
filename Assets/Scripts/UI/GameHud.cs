using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameHud : MenuBase
{
    [SerializeField] private Image[] hearts;

    private int lastHealth = 12;

    private bool canUpdateHealth = true;

    //
    protected override void Initalize()
    {

    }

    protected override IEnumerator PlayHideAnimation()
    {
        OnShowComplete();
        yield return null;
    }

    protected override IEnumerator PlayShowAnimation()
    {
        OnHideComplete();
        yield return null;
    }

    /// <summary>
    /// Updates the hearts on the Hud
    /// </summary>
    /// <param name="_currentHealth">A number between 0 and max health (12)</param>
    private bool UpdateHearts(int _currentHealth)
    {
        if (!canUpdateHealth) //This check is to ensure the hearts don't rotate too much
            return false;

        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].fillAmount = Mathf.Clamp01(_currentHealth * 0.5f - i);

            //Rotate hearts if they have updated (decreased || increased)
            if ((i >= Mathf.FloorToInt(_currentHealth * 0.5f)
                    && i < Mathf.CeilToInt(lastHealth * 0.5f))
                || (i >= Mathf.FloorToInt(lastHealth * 0.5f)
                    && i < Mathf.CeilToInt(_currentHealth * 0.5f)))
            {
                canUpdateHealth = false;
                hearts[i].transform.parent.DOShakeRotation(GameConstants.Animations.shakeTimeShort, Vector3.forward * 90)
                    .OnComplete(delegate
                    {
                        canUpdateHealth = true;
                    });
            }
        }

        lastHealth = _currentHealth;
        return true;
    }
}

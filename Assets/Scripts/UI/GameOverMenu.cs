using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using DG.Tweening;

public class GameOverMenu : MenuBase
{
    [SerializeField] private GameObject wonPanel;
    [SerializeField] private GameObject losePanel;

    public static EndGameReason endGameReason;

    [Header("Won Panel")]
    [SerializeField] private GameObject swords;
    [SerializeField] private GameObject victory;
    [SerializeField] private GameObject statsPanel;

    protected override void Initalize()
    {

    }

    protected override IEnumerator PlayHideAnimation()
    {
        yield return null;
    }

    protected override IEnumerator PlayShowAnimation()
    {
        yield return GameConstants.WaitTimers.waitForOneSecond;

        swords.transform.localScale = Vector3.zero;
        victory.transform.localScale = Vector3.zero;
        swords.transform.localPosition = Vector3.zero;
        victory.transform.localPosition = Vector3.up * -255.0f;

        victory.SetActive(true);
        statsPanel.SetActive(false);

        wonPanel.SetActive(true);

        swords.transform.DOScale(1, 1f).SetEase(Ease.OutBounce);

        yield return GameConstants.WaitTimers.waitForPointTwo;

        victory.transform.DOScale(1, 1f).SetEase(Ease.OutBounce);

        yield return GameConstants.WaitTimers.waitForOneSecond;
        yield return GameConstants.WaitTimers.waitForPointFive;

        swords.transform.DOLocalMoveY(375, 1.0f).SetEase(Ease.OutSine);
        swords.transform.DOScale(0.5f, 1.0f);

        victory.transform.DOLocalMoveY(-700, 0.5f).SetEase(Ease.InBack);

        yield return GameConstants.WaitTimers.waitForOneSecond;
        victory.gameObject.SetActive(false);

        yield return GameConstants.WaitTimers.waitForPointFive;
        statsPanel.transform.localScale = Vector3.zero;
        statsPanel.SetActive(true);
        statsPanel.transform.DOScale(1, 1.0F).SetEase(Ease.OutBounce);
    }

    public override void Show(Action _onShowComplete)
    {
        PlayerController.Instance.DisableMovement();

        base.Show(_onShowComplete);

        if (endGameReason == EndGameReason.Completed)
        {
            StartCoroutine(PlayShowAnimation());
        }
    }

    public override void Hide(Action _onHideComplete)
    {
        base.Hide(_onHideComplete);
    }
}

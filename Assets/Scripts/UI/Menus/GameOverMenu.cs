using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using DG.Tweening;

public class GameOverMenu : MenuBase
{
    public static EndGameReason? endGameReason = null;

    private GameScore _gameScoreCache;

    [Header("Shared Stuff")]
    [SerializeField] private Image reasonIcon;
    [SerializeField] private Image reasonMessage;

    [SerializeField] private GameObject statsPanel;

    [SerializeField] private Button btnContinue;

    [SerializeField] private TextMeshProUGUI txtScore;
    [SerializeField] private TextMeshProUGUI txtBestScore;
    [SerializeField] private TextMeshProUGUI txtCoins;
    [SerializeField] private TextMeshProUGUI txtXP;
    [SerializeField] private TextMeshProUGUI txtKills;
    [SerializeField] private TextMeshProUGUI txtSomething;


    [Header("Won Panel")]
    [SerializeField] private Sprite sprSwords;
    [SerializeField] private Sprite sprVictory;


    [Header("Lose Panel")]
    [SerializeField] private Sprite sprDeath;
    [SerializeField] private Sprite sprTime;
    [SerializeField] private Sprite sprDefeat;

    //
    protected override void Initalize()
    {
        ResetAll();

        btnContinue.onClick.AddListener(OnContinuePress);
    }

    public override void Show(Action _onShowComplete)
    {
        PlayerController.Instance.DisableMovement();

        base.Show(_onShowComplete);

        reasonIcon.sprite = GetReasonIcon();
        reasonMessage.sprite = GetReasonMessage();

        _gameScoreCache = GameManager.Instance.Score;

        txtScore.text = _gameScoreCache.score.ToString("N000");
        txtBestScore.text = "0101";
        txtCoins.text = $"+{_gameScoreCache.coins}";
        txtXP.text = $"+{_gameScoreCache.xp}";
        txtKills.text = _gameScoreCache.kills.ToString("N0");

        menuPanel.SetActive(true);

        StartCoroutine(PlayShowAnimation());
    }

    public override void Hide(Action _onHideComplete)
    {
        base.Hide(_onHideComplete);

        menuPanel.SetActive(false);

        ResetAll();

        OnHideComplete();
    }

    protected override IEnumerator PlayHideAnimation()
    {
        yield return null;

        OnHideComplete();
    }

    protected override IEnumerator PlayShowAnimation()
    {
        yield return GameConstants.WaitTimers.waitForOneSecond;

        reasonMessage.gameObject.SetActive(true);
        reasonIcon.gameObject.SetActive(true);

        reasonIcon.transform.DOScale(1, 1f).SetEase(Ease.OutBounce);

        yield return GameConstants.WaitTimers.waitForPointTwo;

        reasonMessage.transform.DOScale(1, 1f).SetEase(Ease.OutBounce);

        yield return GameConstants.WaitTimers.waitForOneSecond;
        yield return GameConstants.WaitTimers.waitForPointFive;

        reasonIcon.transform.DOLocalMoveY(450, 1.0f).SetEase(Ease.OutSine);
        reasonIcon.transform.DOScale(0.5f, 1.0f);

        reasonMessage.transform.DOLocalMoveY(-700, 0.5f).SetEase(Ease.InBack);

        statsPanel.SetActive(true);
        statsPanel.transform.DOScale(1, 1.0F);

        yield return GameConstants.WaitTimers.waitForOneSecond;
        reasonMessage.gameObject.SetActive(false);

        OnShowComplete();
    }

    private void ResetAll()
    {
        reasonIcon.transform.localScale = Vector3.zero;
        reasonMessage.transform.localScale = Vector3.zero;
        reasonIcon.transform.localPosition = Vector3.zero;
        reasonMessage.transform.localPosition = Vector3.up * -255.0f;
        statsPanel.transform.localScale = Vector3.zero;

        statsPanel.SetActive(false);
        reasonMessage.gameObject.SetActive(false);
        reasonIcon.gameObject.SetActive(false);
    }

    private Sprite GetReasonIcon()
    {
        if (endGameReason == EndGameReason.Completed)
        {
            return sprSwords;
        }
        else if (endGameReason == EndGameReason.Died)
        {
            return sprDeath;
        }
        else
        {
            return sprTime;
        }
    }

    private Sprite GetReasonMessage()
    {
        return endGameReason == EndGameReason.Completed ? sprVictory : sprDefeat;
    }

    private void OnContinuePress()
    {
        MC.Core.CoreBootLoader.Instance.ChangeSceneCollection((int)GameConstants.SceneCollections.HubWorld);
    }
}

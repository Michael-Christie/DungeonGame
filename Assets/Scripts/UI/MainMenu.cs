using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

using MC.Core;
using MC.DiscordManager;

public class MainMenu : MenuBase
{
    [System.Serializable]
    public class SaveFileStruct
    {
        public GameObject parentObject;
        public GameObject emptyObject;
        public GameObject filledSaveObject;

        public Button btnCreateNew;
        public Button btnSelectSave;
        public Button btnCardPress;

        public TextMeshProUGUI txtPlayerName;
        public TextMeshProUGUI txtPlayerClassType;
        public TextMeshProUGUI txtPlayerCoin;
    }

    [Header("Starting Screen")]
    [SerializeField] private CanvasGroup canvasGroupFade;

    [SerializeField] private GameObject title;
    [SerializeField] private GameObject enterDungeon;
    [SerializeField] private GameObject enterDungeonText;
    [SerializeField] private GameObject bottomBar;

    [SerializeField] private Button btnSettings;
    [SerializeField] private Button btnStartGame;

    [Space]
    [Header("Save Data Screen")]
    [SerializeField] private GameObject userSelectBackground;

    [SerializeField] private SaveFileStruct[] saveSlots;

    [SerializeField] private Button btnBackToMenu;
    [SerializeField] private Button btnCreateNew;
    [SerializeField] private Button btnCancelNew;

    [SerializeField] private CanvasGroup newSaveCanvasFade;

    [SerializeField] private TMP_InputField userName;

    private int saveSlotSelected = -1;

    public PlayerData playerLoadedData { get; private set; }

    //
    private void Start()
    {
        Show();
    }

    protected override void Initalize()
    {
        if (isInitalized)
            return;
        isInitalized = true;

        AudioManager.Instance.PlayMusic(GameConstants.MusicClip.MainMenu);

        MCDiscordManager.Instance.Create(
            delegate (bool _success)
            {
                if (_success)
                {
                    MCDiscordManager.Instance.SetActivity("Main Menu", "");
                }
            });

        btnSettings.onClick.AddListener(OnSettingsClicked);
        btnStartGame.onClick.AddListener(GameStart);
        btnBackToMenu.onClick.AddListener(BackToMenu);
        btnCancelNew.onClick.AddListener(CancelNewSave);
        btnCreateNew.onClick.AddListener(CreateSave);

        for (int i = 0; i < saveSlots.Length; i++)
        {
            int _currentIndex = i;

            saveSlots[i].btnCreateNew.onClick.AddListener(
                delegate
                {
                    CreateNewSave(_currentIndex);
                });

            saveSlots[i].btnSelectSave.onClick.AddListener(
                delegate
                {
                    LoadGameWithPlayer(_currentIndex);
                });

            saveSlots[i].btnCardPress.onClick.AddListener(
                delegate
                {
                    if (saveSlots[_currentIndex].emptyObject.activeInHierarchy)
                    {
                        CreateNewSave(_currentIndex);
                    }
                    else
                    {
                        LoadGameWithPlayer(_currentIndex);
                    }
                });
        }

        canvasGroupFade.DOFade(1, GameConstants.Animations.fadeTime);

        StartCoroutine(LoadPlayersData());
    }

    public override void Show(Action _onFinishShow = null)
    {
        base.Show(_onFinishShow);

        title.transform.localScale = Vector3.zero;
        enterDungeon.transform.localScale = Vector3.zero;
        btnSettings.transform.localScale = Vector3.zero;
        bottomBar.transform.localPosition = new Vector3(0, -850, 0);

        menuPanel.SetActive(true);

        StartCoroutine(PlayShowAnimation());
    }

    public override void Hide(Action _onFinishHide = null)
    {
        base.Hide(_onFinishHide);

        menuPanel.SetActive(false);

        OnHideComplete();
    }

    protected override IEnumerator PlayShowAnimation()
    {
        yield return GameConstants.WaitTimers.waitForFade;

        title.transform.DOScale(Vector3.one, GameConstants.Animations.scaleTime)
                .SetEase(Ease.OutBounce);
        yield return GameConstants.WaitTimers.waitForOneSecond;

        bottomBar.transform.DOLocalMoveY(-540F, GameConstants.Animations.moveTime)
            .SetEase(Ease.OutCubic);
        yield return GameConstants.WaitTimers.waitForMove;

        enterDungeon.transform.DOScale(Vector3.one, GameConstants.Animations.scaleTime)
            .SetEase(Ease.OutBounce);

        yield return GameConstants.WaitTimers.waitForScale;

        btnSettings.transform.DOScale(Vector3.one, GameConstants.Animations.scaleTime)
            .SetEase(Ease.OutBack);
        yield return GameConstants.WaitTimers.waitForScale;

        OnShowComplete();
    }

    private void OnSettingsClicked()
    {
        AudioManager.Instance.PlaySoundEffect(GameConstants.SoundClip.ButtonPress);
        CoreBootLoader.Instance.AddScene((int)GameConstants.Scenes.Settings);
    }

    private void GameStart()
    {
        enterDungeonText.SetActive(false); //could have the text fade maybe?
        btnStartGame.enabled = false;

        AudioManager.Instance.PlaySoundEffect(GameConstants.SoundClip.ButtonPress);

        userSelectBackground.transform.localScale = Vector3.zero;

        for (int i = 0; i < saveSlots.Length; i++)
        {
            saveSlots[i].parentObject.transform.localRotation = Quaternion.Euler(Vector3.up * 90);
        }

        userSelectBackground.SetActive(true);
        StartCoroutine(GotoUserSelect());
    }

    private IEnumerator GotoUserSelect()
    {
        enterDungeon.transform.DOScale(Vector3.one * 20, GameConstants.Animations.scaleTime)
            .SetEase(Ease.OutBounce);

        yield return GameConstants.WaitTimers.waitForScaleShort;

        userSelectBackground.transform.DOScale(Vector3.one, GameConstants.Animations.scaleTime);

        yield return GameConstants.WaitTimers.waitForScale;

        canvasGroupFade.gameObject.SetActive(false);

        for (int i = 0; i < saveSlots.Length; i++)
        {
            saveSlots[i].parentObject.transform.DOLocalRotate(Vector3.zero, GameConstants.Animations.flipTime, RotateMode.Fast).
                SetEase(Ease.OutBounce);
        }
        yield return GameConstants.WaitTimers.waitForFlipShort;
    }

    private void BackToMenu()
    {
        AudioManager.Instance.PlaySoundEffect(GameConstants.SoundClip.ButtonPress);
        StartCoroutine(GoToMenu());
    }

    private IEnumerator GoToMenu()
    {
        canvasGroupFade.gameObject.SetActive(true);
        userSelectBackground.transform.DOScale(Vector3.zero, GameConstants.Animations.scaleTimeShort);

        yield return GameConstants.WaitTimers.waitForScaleShort;

        enterDungeon.transform.DOScale(Vector3.one, GameConstants.Animations.scaleTimeShort)
            .SetEase(Ease.OutBounce);
        yield return GameConstants.WaitTimers.waitForScaleShort;

        userSelectBackground.SetActive(false);

        enterDungeonText.SetActive(true);
        btnStartGame.enabled = true;
    }

    private IEnumerator LoadPlayersData()
    {
        PlayerData _tempData;
        SaveFileHelper.LoadDataXML(GameConstants.SaveFiles.PlayerOneSave, out _tempData);
        ConfigurePlayerSaveCards(0, _tempData);

        yield return GameConstants.WaitTimers.waitForPointOne;

        SaveFileHelper.LoadDataXML(GameConstants.SaveFiles.PlayerTwoSave, out _tempData);
        ConfigurePlayerSaveCards(1, _tempData);

        yield return GameConstants.WaitTimers.waitForPointOne;

        SaveFileHelper.LoadDataXML(GameConstants.SaveFiles.PlayerThreeSave, out _tempData);
        ConfigurePlayerSaveCards(2, _tempData);
    }

    private void ConfigurePlayerSaveCards(int _cardIndex, PlayerData _data)
    {
        if (_data == null) //There is no save file
        {
            saveSlots[_cardIndex].emptyObject.SetActive(true);
            saveSlots[_cardIndex].filledSaveObject.SetActive(false);
        }
        else //There is a save File
        {
            saveSlots[_cardIndex].emptyObject.SetActive(false);
            saveSlots[_cardIndex].filledSaveObject.SetActive(true);

            saveSlots[_cardIndex].txtPlayerName.text = _data.name;
            saveSlots[_cardIndex].txtPlayerClassType.text = _data.charType;
            saveSlots[_cardIndex].txtPlayerCoin.text = _data.coin.ToString("N000");
        }
    }

    private void CreateNewSave(int _saveSlot)
    {
        AudioManager.Instance.PlaySoundEffect(GameConstants.SoundClip.ButtonPress);

        saveSlotSelected = _saveSlot;

        userName.text = "";

        newSaveCanvasFade.gameObject.SetActive(true);
        newSaveCanvasFade.DOFade(1, GameConstants.Animations.fadeTimeShort);
    }

    private void CreateSave()
    {
        if (saveSlotSelected == -1)
            return;

        PlayerData _temp = new PlayerData();

        _temp.name = userName.text;
        _temp.charType = "Wizard";
        _temp.coin = 100;

        SaveFileHelper.SaveDataXML(GetSavePathFromInt(saveSlotSelected), _temp);

        ConfigurePlayerSaveCards(saveSlotSelected, _temp);

        CancelNewSave();
    }

    private void CancelNewSave()
    {
        AudioManager.Instance.PlaySoundEffect(GameConstants.SoundClip.ButtonPress);

        saveSlotSelected = -1;

        newSaveCanvasFade.DOFade(0, GameConstants.Animations.fadeTimeShort)
            .onComplete =
                delegate
                {
                    newSaveCanvasFade.gameObject.SetActive(false);
                };
    }

    private void LoadGameWithPlayer(int _saveSlot)
    {
        AudioManager.Instance.PlaySoundEffect(GameConstants.SoundClip.ButtonPress);

        PlayerData _player;
        SaveFileHelper.LoadDataXML(GetSavePathFromInt(_saveSlot), out _player);

        GameData.Instance.SetPlayerData(_player);

        CoreBootLoader.Instance.ChangeSceneCollection((int)GameConstants.SceneCollections.HubWorld);
    }

    private string GetSavePathFromInt(int _saveSlot)
    {
        if (_saveSlot == 0)
        {
            return GameConstants.SaveFiles.PlayerOneSave;
        }
        else if (_saveSlot == 1)
        {
            return GameConstants.SaveFiles.PlayerTwoSave;
        }
        else if (_saveSlot == 2)
        {
            return GameConstants.SaveFiles.PlayerThreeSave;
        }

        Debug.LogError($"Load Save System | Save slot {_saveSlot} is not in Range", this);
        return GameConstants.SaveFiles.ErrorSave;
    }
}

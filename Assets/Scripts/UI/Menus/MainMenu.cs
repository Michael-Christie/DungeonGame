using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

using MC.Core;
using MC.DiscordManager;

using Image = UnityEngine.UI.Image;

namespace UI.Menus
{
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
            public TextMeshProUGUI txtPlayerCoin;

            public Image imgClassIcon;
        }

        [Header("Starting Screen")]
        [SerializeField] private CanvasGroup canvasGroupFade;

        [SerializeField] private GameObject title;
        [SerializeField] private GameObject enterDungeon;
        [SerializeField] private GameObject enterDungeonText;
        [SerializeField] private GameObject bottomBar;

        [SerializeField] private Button btnSettings;
        [SerializeField] private Button btnStartGame;
        [SerializeField] private Button btnDiscord;
        [SerializeField] private Button btnTwitter;

        [Space]
        [Header("Save Data Screen")]
        [SerializeField] private GameObject userSelectBackground;

        [SerializeField] private GameObject saveTitle;
        [SerializeField] private GameObject saveBack;

        [SerializeField] private SaveFileStruct[] saveSlots;

        [SerializeField] private Button btnBackToMenu;
        [SerializeField] private Button btnCreateNew;
        [SerializeField] private Button btnCancelNew;

        [SerializeField] private CanvasGroup newSaveCanvasFade;

        [SerializeField] private Toggle togNormal;
        [SerializeField] private Toggle togScout;
        [SerializeField] private Toggle togMage;
        [SerializeField] private Toggle togHeavy;
        [SerializeField] private Toggle togOther;

        [SerializeField] private TextMeshProUGUI txtClassTitle;
        [SerializeField] private TextMeshProUGUI txtClassDescription;

        [SerializeField] private Sprite[] sprClassIcons;

        private string[] classDescriptions =
        {
        /*Hunter*/"Hunter's is the default class. The class is balanced overall.", //Normal
        /*Scout*/"Scout's has increased movement speed and a quieter step, making it harder for enemies to hear you. However because of your light weight, you deal less damage. \n\nIf you like to sneak around and stealth into places, this is the class for you.", //Stealth
        /*Heavy*/"The Heavy class has got a real punch to them, dealing more damange per hit, but is slower and enemies can hear you from further away. \n\nIf you like to brute force your way through combat, this is the class for you.", //Brutes
        /*Mage*/"The Mage is a balanced class who makes use of magic to attack from a distance. Meele Damage is reduced but movement speed is increased. \n\nIf you like to attack from a distance, this is the class for you", //Gun Mage (Magic plus rage)
        /*Bard*/"The Bard Class has increased speed and damage, however when hit takes extra damage." //Bit of everything 
    };

        private GameConstants.CharacterTypes selectedType;

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

            btnDiscord.onClick.AddListener(OpenDiscord);
            btnTwitter.onClick.AddListener(OpenTwitter);

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

            togNormal.onValueChanged.AddListener(
                delegate (bool _value)
                {
                    if (_value)
                    {
                        SelectedClass(GameConstants.CharacterTypes.Hunter);
                    }
                });

            togScout.onValueChanged.AddListener(
                delegate (bool _value)
                {
                    if (_value)
                    {
                        SelectedClass(GameConstants.CharacterTypes.Scout);
                    }
                });

            togHeavy.onValueChanged.AddListener(
                delegate (bool _value)
                {
                    if (_value)
                    {
                        SelectedClass(GameConstants.CharacterTypes.Heavy);
                    }
                });

            togMage.onValueChanged.AddListener(
                delegate (bool _value)
                {
                    if (_value)
                    {
                        SelectedClass(GameConstants.CharacterTypes.Mage);
                    }
                });

            togOther.onValueChanged.AddListener(
                delegate (bool _value)
                {
                    if (_value)
                    {
                        SelectedClass(GameConstants.CharacterTypes.Bard);
                    }
                });

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

        }

        private void GameStart()
        {
            enterDungeonText.SetActive(false);
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
            if (_data.Equals(default(PlayerData))) //There is no save file
            {
                saveSlots[_cardIndex].emptyObject.SetActive(true);
                saveSlots[_cardIndex].filledSaveObject.SetActive(false);
            }
            else //There is a save File
            {
                saveSlots[_cardIndex].emptyObject.SetActive(false);
                saveSlots[_cardIndex].filledSaveObject.SetActive(true);

                saveSlots[_cardIndex].txtPlayerName.text = _data.charType.ToString();
                saveSlots[_cardIndex].txtPlayerCoin.text = _data.coin.ToString("N000");

                saveSlots[_cardIndex].imgClassIcon.sprite = sprClassIcons[(int)_data.charType];
            }
        }

        private void CreateNewSave(int _saveSlot)
        {
            AudioManager.Instance.PlaySoundEffect(GameConstants.SoundClip.ButtonPress);

            saveSlotSelected = _saveSlot;

            togNormal.isOn = true;
            SelectedClass(GameConstants.CharacterTypes.Hunter);

            newSaveCanvasFade.gameObject.SetActive(true);
            newSaveCanvasFade.DOFade(1, GameConstants.Animations.fadeTimeShort)
                .OnComplete(
                    delegate
                    {
                        ToggleCardContent(false);
                    });
        }

        private void ToggleCardContent(bool _isActive)
        {
            for (int i = 0; i < saveSlots.Length; i++)
            {
                saveSlots[i].parentObject.SetActive(_isActive);
            }

            saveTitle.SetActive(_isActive);
            saveBack.SetActive(_isActive);
        }

        private void CreateSave()
        {
            if (saveSlotSelected == -1)
                return;

            PlayerData _temp = new PlayerData();

            _temp.charType = selectedType;
            _temp.coin = 100;

            SaveFileHelper.SaveDataXML(GameConstants.GetSavePathFromInt(saveSlotSelected), _temp);

            ConfigurePlayerSaveCards(saveSlotSelected, _temp);

            CancelNewSave();
        }

        private void CancelNewSave()
        {
            AudioManager.Instance.PlaySoundEffect(GameConstants.SoundClip.ButtonPress);

            saveSlotSelected = -1;

            ToggleCardContent(true);

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

            SaveFileHelper.LoadDataXML(GameConstants.GetSavePathFromInt(_saveSlot), out PlayerData _player);

            GameData.Instance.SetPlayerData(_player, _saveSlot);

            CoreBootLoader.Instance.ChangeSceneCollection((int)GameConstants.SceneCollections.HubWorld);
        }

        private void SelectedClass(GameConstants.CharacterTypes _type)
        {
            selectedType = _type;
            txtClassTitle.text = _type.ToString();
            txtClassDescription.text = classDescriptions[(int)_type];
        }

        #region Social Media Links
        private void OpenDiscord()
        {
            MCDiscordManager.Instance.RequestInviteToDiscordServer();
        }

        private void OpenTwitter()
        {
            Application.OpenURL("https://twitter.com/MichaelChriste");
        }
        #endregion
    }
}
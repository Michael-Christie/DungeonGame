using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace UI.Menus
{
    public class SettingsMenu : MenuBase
    {
        [SerializeField] private Button btnClose;

        [SerializeField] private GameObject contentPanel;

        [SerializeField] private CanvasGroup fadeGroup;

        //
        protected override void Initalize()
        {
            btnClose.onClick.AddListener(CloseMenu);
        }

        public override void Show(Action _onFinishShow = null)
        {
            base.Show(_onFinishShow);

            contentPanel.transform.localScale = Vector3.zero;

            PlayerController.Instance?.DisableMovement();

            menuPanel.SetActive(true);

            StartCoroutine(PlayShowAnimation());
        }

        protected override IEnumerator PlayShowAnimation()
        {
            //fadeGroup.DOFade(1, GameConstants.Animations.fadeTimeShort);
            //yield return GameConstants.WaitTimers.waitForFadeShort;

            contentPanel.transform.DOScale(Vector3.one, GameConstants.Animations.scaleTimeShort);
            yield return GameConstants.WaitTimers.waitForScale;

            OnShowComplete();
        }

        public override void Hide(Action _onFinishHide = null)
        {
            base.Hide(_onFinishHide);

            StartCoroutine(PlayHideAnimation());
        }

        protected override IEnumerator PlayHideAnimation()
        {
            contentPanel.transform.DOScale(Vector3.zero, GameConstants.Animations.scaleTimeShort);
            yield return GameConstants.WaitTimers.waitForScaleShort;

            //fadeGroup.DOFade(0, GameConstants.Animations.fadeTimeShort);
            //yield return GameConstants.WaitTimers.waitForFadeShort;

            menuPanel.SetActive(false);

            PlayerController.Instance?.EnableMovement();

            OnHideComplete();

            //MC.Core.CoreBootLoader.Instance.RemoveScene((int)GameConstants.Scenes.Settings);
        }

        private void CloseMenu()
        {
            AudioManager.Instance.PlaySoundEffect(GameConstants.SoundClip.ButtonPress);
            //Hide();
            MenuManager.Instance.HideMenu();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;
using TMPro;

namespace UI.Menus
{
    public class GameHud : MenuBase
    {
        [SerializeField] private TextMeshProUGUI txtTimer;

        [SerializeField] private Slider healthBar;

        //
        protected override void Initalize()
        {
            PlayerController.Instance.onHealthUpdate += UpdateHealth;
            healthBar.maxValue = PlayerController.Instance.playerClass.Health;
        }

        public override void Show(Action _onShowComplete)
        {
            base.Show(_onShowComplete);

            menuPanel.SetActive(true);

            healthBar.value = PlayerController.Instance.Health;

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
        private void UpdateHealth(int _currentHealth)
        {
            healthBar.DOValue(_currentHealth, GameConstants.Animations.shakeTimeShort);
        }

        public void UpdateTimer(GameTime _time)
        {
            txtTimer.text = _time.ToString();
        }
    }
}

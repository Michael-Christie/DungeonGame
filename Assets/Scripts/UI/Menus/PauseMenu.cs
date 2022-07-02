using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MenuBase
{
    [SerializeField] private Button btnResume; 
    [SerializeField] private Button btnSettings; 
    [SerializeField] private Button btnQuitMenu; 
    [SerializeField] private Button btnQuitDesktop; 

    //
    protected override void Initalize()
    {
        btnResume.onClick.AddListener(OnResumePressed);
        btnSettings.onClick.AddListener(OnSettingPressed);
        btnQuitDesktop.onClick.AddListener(OnQuitDesktop);
        btnQuitMenu.onClick.AddListener(OnQuiteMenu);
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
        MenuManager.Instance.HideMenu();
    }

    private void OnResumePressed()
    {
        MenuManager.Instance.HideMenu();
    }

    private void OnSettingPressed()
    {
        MenuManager.Instance.ShowMenu((int)GameConstants.Menus.Settings);
    }

    private void OnQuiteMenu()
    {

    }

    private void OnQuitDesktop()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}

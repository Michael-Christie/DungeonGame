using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditorBlockSelector : MenuBase
{
    [Header("HotBar")]
    [SerializeField] private Toggle[] toggles;

    private int indexSelected = 0;

    //
    protected override void Initalize()
    {

    }

    public override void Show(Action _onShowComplete)
    {
        base.Show(_onShowComplete);
        OnShowComplete();

        menuPanel.SetActive(true);
    }

    public override void Hide(Action _onHideComplete)
    {
        base.Hide(_onHideComplete);
        OnHideComplete();
    }
}

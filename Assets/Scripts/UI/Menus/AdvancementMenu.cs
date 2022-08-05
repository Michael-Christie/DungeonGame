using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdvancementMenu : MenuBase
{
    protected override void Initalize()
    {

    }

    public override void Show(Action _onShowComplete)
    {
        base.Show(_onShowComplete);
        OnShowComplete();
    }
}

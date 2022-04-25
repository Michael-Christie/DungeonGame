using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryMenu : MenuBase
{
    private const int inventoryHeight = 5;
    private const int inventoryWidth = 5;

    [SerializeField] private InventorySlot[] inventorySlots;

    //
    protected override void Initalize()
    {
        
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

    private void PopulateInventoryMenu()
    {
        //Get the player data, to pull inventoryData
    }
}

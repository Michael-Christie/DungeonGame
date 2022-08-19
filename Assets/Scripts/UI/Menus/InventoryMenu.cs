using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryMenu : MenuBase
{
    private const int inventoryHeight = 5;
    private const int inventoryWidth = 5;

    [SerializeField] private InventorySlot[] inventorySlots;

    [SerializeField] private Toggle[] hotbarItems;

    [SerializeField] private Image[] hotbarImage;

    //
    protected override void Initalize()
    {
        isInitalized = true;

        PlayerInventory.Instance.onHotbarUpdate += UpdateHotbar;
        PlayerInventory.Instance.onInventoryUpdate += PopulateInventoryMenu;

        PopulateInventoryMenu();
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
        Inventory[] _inventory = PlayerInventory.Instance.Inventory;

        //Get the player data, to pull inventoryData
        for(int i = 0; i < inventorySlots.Length; i++)
        {
            inventorySlots[i].SetUp(_inventory[i].itemData, _inventory[i].amount);

            if(i < hotbarImage.Length)
            {
                if (_inventory[i].itemData)
                {
                    hotbarImage[i].overrideSprite = _inventory[i].itemData.itemIcon;
                }
                else
                {
                    hotbarImage[i].overrideSprite = null;
                }
            }
        }
    }

    private void UpdateHotbar(int _index)
    {
        hotbarItems[_index].isOn = true;
    }
}

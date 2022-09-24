using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EditorMenu : MenuBase
{
    [SerializeField] private Button btnSave;
    [SerializeField] private Button btnLoad;
    [SerializeField] private Button btnQuit;

    [SerializeField] private GameObject saveWindow;
    [SerializeField] private GameObject loadWindow;

    [Header("Save Window")]
    [SerializeField] private TMP_InputField inputField;

    [SerializeField] private Button btnSaveRoom;
    [SerializeField] private Button btnCancelRoom;

    //
    protected override void Initalize()
    {
        btnSave.onClick.AddListener(ShowSaveWindow);
        btnLoad.onClick.AddListener(ShowLoadMenu);

        btnSaveRoom.onClick.AddListener(SaveRoom);
        btnCancelRoom.onClick.AddListener(CancelRoom);
    }

    public override void Show(Action _onShowComplete)
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;

        base.Show(_onShowComplete);
        OnShowComplete();

        menuPanel.SetActive(true);
    }

    public override void Hide(Action _onHideComplete)
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        base.Hide(_onHideComplete);
        OnHideComplete();

        EditorManager.Instance.canMove = false;
    }

    private void ShowSaveWindow()
    {
        saveWindow.SetActive(true);
    }

    private void ShowLoadMenu()
    {
        loadWindow.SetActive(true);
    }

    private void SaveRoom()
    {
        if (string.IsNullOrEmpty(inputField.text))
        {
            return;
        }

        EditorManager.Instance.assetName = inputField.text;

        EditorManager.Instance.SaveAsPrefab();

        CancelRoom();
    }

    private void CancelRoom()
    {
        saveWindow.SetActive(false);
    }
}

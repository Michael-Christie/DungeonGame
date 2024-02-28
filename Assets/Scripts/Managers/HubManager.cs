using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using MC.Core;

/// <summary>
/// The Hub manager is responsible for managing the game in the game hub world.
/// </summary>
public class HubManager : MonoBehaviour
{
    public static HubManager Instance { get; private set; }

    private bool isPortalAlive = true;

    [SerializeField] private Material matPortal;

    private int portalAlphaFade = Shader.PropertyToID("AlphaFade");

    //
    private void Awake()
    {
        Instance = this;

        //TurnOffPortal();
    }

    public void StartGameCountDown()
    {
        if (!isPortalAlive)
            return;

        Debug.Log("Starting Game");
        //Do some animation and then start the game.
        CoreBootLoader.Instance.ChangeSceneCollection((int)GameConstants.SceneCollections.Game);
    }

    public void TurnOffPortal()
    {
        matPortal.DOFloat(1, portalAlphaFade, 1);

        isPortalAlive = false;
    }

    public void TurnOnPortal()
    {
        matPortal.DOFloat(0, portalAlphaFade, 2.5f);

        isPortalAlive = true;
    }
}

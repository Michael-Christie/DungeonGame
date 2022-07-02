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

    private bool isStartingGame;

    [SerializeField] private GameObject leftDoor; 
    [SerializeField] private GameObject rightDoor;

    [SerializeField] private Material matPortal;

    private int portalAlphaFade = Shader.PropertyToID("AlphaFade");

    //
    private void Awake()
    {
        Instance = this;
        //portalAlphaFade = Shader.PropertyToID("AlphaFade");

        TurnOffPortal();
    }

    public void StartGameCountDown()
    {
        Debug.Log("Starting Countdown");
        isStartingGame = true;

        StartCoroutine(GameStartCoutdown());
    }

    public void StopGameCountDown()
    {
        Debug.Log("Stopping Countdown");
        isStartingGame = false;
    }

    private IEnumerator GameStartCoutdown()
    {
        for (int i = 0; i < 3; i++)
        {
            HubHud.ShowCountdownDigit?.Invoke(3 - i);

            yield return GameConstants.WaitTimers.waitForOneSecond;
            yield return GameConstants.WaitTimers.waitForPointFive;

            if(!isStartingGame)
            {
                yield break;
            }
        }

        leftDoor.transform.DOLocalRotate(Vector3.zero, GameConstants.Animations.rotateTime);
        rightDoor.transform.DOLocalRotate(Vector3.zero, GameConstants.Animations.rotateTime);

        TurnOnPortal();

        yield return GameConstants.WaitTimers.waitForRotate;

        yield return GameConstants.WaitTimers.waitForOneSecond;

        StartGame();
    }

    private void StartGame()
    {
        Debug.Log("Starting Game");
        //Do some animation and then start the game.
        CoreBootLoader.Instance.ChangeSceneCollection((int)GameConstants.SceneCollections.Game);
    }

    private void TurnOffPortal()
    {
        matPortal.DOFloat(1, portalAlphaFade, 1);
    }

    private void TurnOnPortal()
    {
        matPortal.DOFloat(0, portalAlphaFade, 2.5f);
    }
}

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

    [SerializeField] private HubUI hubUI;

    [SerializeField] private GameObject leftDoor; 
    [SerializeField] private GameObject rightDoor; 

    //
    private void Awake()
    {
        Instance = this;
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
            hubUI.ShowCoutdown((3 - i).ToString());

            yield return GameConstants.WaitTimers.waitForOneSecond;
            yield return GameConstants.WaitTimers.waitForPointFive;

            if(!isStartingGame)
            {
                yield break;
            }
        }

        leftDoor.transform.DOLocalRotate(Vector3.zero, GameConstants.Animations.rotateTime);
        rightDoor.transform.DOLocalRotate(Vector3.zero, GameConstants.Animations.rotateTime);

        yield return GameConstants.WaitTimers.waitForRotate;

        yield return GameConstants.WaitTimers.waitForOneSecond;

        StartGame();
    }

    private void StartGame()
    {
        leftDoor.transform.DOLocalRotate(Vector3.zero, GameConstants.Animations.rotateTime);
        rightDoor.transform.DOLocalRotate(Vector3.zero, GameConstants.Animations.rotateTime);


        Debug.Log("Starting Game");
        //Do some animation and then start the game.
        CoreBootLoader.Instance.ChangeSceneCollection((int)GameConstants.SceneCollections.Game);
    }
}

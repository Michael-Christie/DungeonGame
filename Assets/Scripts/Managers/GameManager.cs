using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;

public enum EndGameReason
{
    Completed,
    OutOfTime,
    Died
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private DateTime startTime;
    private DateTime endTime;

    private GameTime timeCache = new GameTime();

    private GameScore gameScore = new GameScore();

    [SerializeField] private Transform[] artefactSpawnPlace;

    [SerializeField] private GameObject leftDoor;
    [SerializeField] private GameObject rightDoor;

    private bool collectedGoal;
    private bool endGameCountdown;

    public GameTime CurrentTime
    {
        get
        {
            TimeSpan _span = DateTime.UtcNow - startTime;

            timeCache.minuets = _span.Minutes;
            timeCache.seconds = _span.Seconds;

            return timeCache;
        }
    }

    public GameTime TimeRemaining
    {
        get
        {
            TimeSpan _span = endTime - DateTime.UtcNow;

            timeCache.minuets = _span.Minutes;
            timeCache.seconds = _span.Seconds;

            return timeCache;
        }
    }

    public GameScore GameScore
    {
        get
        {
            return gameScore;
        }
    }

    //
    private void Awake()
    {
        Instance = this;
    }

    private IEnumerator SlowUpdateCheck()
    {
        while(DateTime.UtcNow < endTime)
        {
            yield return GameConstants.WaitTimers.waitForOneSecond;

        }

        EndGame(EndGameReason.OutOfTime);
    }

    public void StartGame()
    {
        startTime = DateTime.UtcNow;
        endTime = startTime + TimeSpan.FromSeconds(30);

        collectedGoal = false;

        //Spawn the artefact somewhere...
        IPoolable _pooledObject = ObjectPooler.Instance.GetObject((int)GameConstants.EntityID.Artefact);

        int _rndIndex = UnityEngine.Random.Range(0, artefactSpawnPlace.Length);
        _pooledObject.SetPosition(artefactSpawnPlace[_rndIndex].transform, artefactSpawnPlace[_rndIndex].position);

        StartCoroutine(SlowUpdateCheck());
    }

    public void ArtefactCollected()
    {
        collectedGoal = true;
    }

    public void EndGame(EndGameReason _reason)
    {
        if (GameOverMenu.endGameReason != null)
            return;

        StopAllCoroutines();

        GameOverMenu.endGameReason = _reason;
        MenuManager.Instance.ShowMenu((int)GameConstants.Menus.GameOver);
    }

    public void StartEndGame()
    {
        endGameCountdown = true;

        StartCoroutine(WaitForEndGame());
    }

    public void CancelEndGame()
    {
        endGameCountdown = false;
    }

    private IEnumerator WaitForEndGame()
    {
        for (int i = 0; i < 3; i++)
        {
            //hubUI.ShowCoutdown((3 - i).ToString());

            yield return GameConstants.WaitTimers.waitForOneSecond;
            yield return GameConstants.WaitTimers.waitForPointFive;

            if (!endGameCountdown)
            {
                yield break;
            }
        }

        leftDoor.transform.DOLocalRotate(Vector3.zero, GameConstants.Animations.rotateTime);
        rightDoor.transform.DOLocalRotate(Vector3.zero, GameConstants.Animations.rotateTime);

        yield return GameConstants.WaitTimers.waitForRotate;

        yield return GameConstants.WaitTimers.waitForOneSecond;

        EndGame(EndGameReason.Completed);
    }
}

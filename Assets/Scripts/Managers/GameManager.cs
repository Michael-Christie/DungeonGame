using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EndGameReason
{
    Completed,
    OutOfTime,
    Died
}

public class GameManager : MonoBehaviour
{
    public struct GameTime
    {
        public int seconds;
        public int minuets;

        //
        public override string ToString()
        {
            return $"{minuets}:{seconds}";
        }
    }

    public static GameManager Instance { get; private set; }

    private DateTime startTime;
    private DateTime endTime;

    private GameTime timeCache = new GameTime();

    [SerializeField] private Transform[] artefactSpawnPlace;

    private bool collectedGoal;

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
        endTime = startTime + TimeSpan.FromMinutes(5);

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
        GameOverMenu.endGameReason = _reason;
        MenuManager.Instance.ShowMenu((int)GameConstants.Menus.GameOver);
    }
}

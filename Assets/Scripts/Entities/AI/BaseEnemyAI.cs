using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState
{
    Idle,
    Chasing,
    Attacking
}

public class BaseEnemyAI : BaseEntity, IPoolable, IDamageable
{
    public int PoolID => (int)GameConstants.EntityID.BaseAI;

    public bool IsInScene { get; set; }

    public int Health { get; set; } = 100;

    [SerializeField] private NavMeshAgent navMeshAgent;

    public EnemyState currentState { get; private set; } = EnemyState.Idle;

    protected float sightDistance = 100;

    [SerializeField] private LayerMask hitMask;

    private Vector3? lastSeenPosition;

    [SerializeField] private Material redFlash;
    [SerializeField] private Material whiteDefault;

    [SerializeField] private MeshRenderer meshRenderer;

    //
    #region Interfaces
    ///#IPoolable
    public void ReturnToPool()
    {
        navMeshAgent.enabled = false;

        gameObject.SetActive(false);
        IsInScene = false;

        gameObject.transform.SetParent(ObjectPooler.Instance.transform);

        currentState = EnemyState.Idle;
    }

    public void SetPosition(Transform _newParent)
    {
        SetPosition(_newParent, _newParent.position);
    }

    public void SetPosition(Transform _newParent, Vector3 _position)
    {
        transform.SetParent(_newParent);
        transform.position = _position;

        gameObject.SetActive(true);

        navMeshAgent.enabled = true;
    }

    ///#IDamagable
    public void OnDamageRecieved(int _damageAmount)
    {
        Health -= _damageAmount;

        if(Health > 0)
        {
            StartCoroutine(IChangeColor());

            return;
        }

        //Entity has been killed..
        GameManager.Instance.AddKillToScore();
        ReturnToPool();
    }
    #endregion

    //Temp update function, OnEntityUpdate should be controlled by game manager
    private void FixedUpdate()
    {
        OnEntityUpdate();
    }

    public void OnEntityUpdate()
    {
        if (CanSeePlayer())
        {
            currentState = EnemyState.Chasing;
            navMeshAgent.isStopped = false;
        }
        else
        {
            currentState = EnemyState.Idle;
            IdleUpdate();
        }

        if (lastSeenPosition.HasValue)
        {
            navMeshAgent.SetDestination(lastSeenPosition.Value);

            if (Vector3.Distance(transform.position, lastSeenPosition.Value) < 2.5f)
            {
                navMeshAgent.isStopped = true;

                lastSeenPosition = null;

                currentState = EnemyState.Idle;
            }
        }
    }
    
    private bool CanSeePlayer()
    {
        Ray _ray = new Ray(transform.position, (PlayerController.Instance.PlayerCamera.transform.position - transform.position).normalized);
        if(Physics.Raycast(_ray, out RaycastHit _hit, sightDistance, hitMask))
        {
            if (_hit.collider.CompareTag(GameConstants.Tags.player))
            {
                //hit the player...
                lastSeenPosition = _hit.collider.transform.position;

                return true;
            }
        }

        return false;
    }

    private void IdleUpdate()
    {
        //Do small movements basically
    }

    private IEnumerator IChangeColor()
    {
        meshRenderer.material = redFlash;
        yield return GameConstants.WaitTimers.waitForPointFive;
        meshRenderer.material = whiteDefault;
    }
}

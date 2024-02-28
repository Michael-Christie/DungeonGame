using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UI.Menus;

public enum EnemyState
{
    Idle,
    Follow,
    Attacking
}

public class BaseEnemyAI : BaseEntity, IPoolable, IDamageable
{
    public virtual int PoolID => (int)GameConstants.EntityID.BaseAI;

    public bool IsInScene { get; set; }

    public virtual int Health { get; set; } = 100;

    [SerializeField] private NavMeshAgent navMeshAgent;

    public EnemyState CurrentState { get; private set; } = EnemyState.Idle;

    [SerializeField] protected float sightDistance = 100;
    [SerializeField] protected float movementSpeed = 3.5f;
    protected float distanceToTargetPos;

    private bool sawPlayer;

    [SerializeField] private LayerMask hitMask;

    private Vector3? lastSeenPosition;

    private Vector3 _targetPos;
    protected Vector3 TargetPosition
    {
        get
        {
            return _targetPos;
        }
        set
        {
            _targetPos = value;
            navMeshAgent.SetDestination(_targetPos);
            navMeshAgent.isStopped = false;
        }
    }

    [SerializeField] private Material redFlash;
    [SerializeField] private Material whiteDefault;

    [SerializeField] private MeshRenderer meshRenderer;

    //Idle Stuff
    private float idleTime;

    private bool isIdleMoving;

    //
    #region Interfaces
    ///#IPoolable
    public void ReturnToPool()
    {
        navMeshAgent.enabled = false;

        gameObject.SetActive(false);
        IsInScene = false;

        gameObject.transform.SetParent(ObjectPooler.Instance.transform);

        CurrentState = EnemyState.Idle;
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

        _targetPos = _position;

        AIManager.Instance?.RegisterAgent(this);
    }

    ///#IDamagable
    public void OnDamageRecieved(int _damageAmount)
    {
        Debug.Log($"Dealing {_damageAmount} damage");
        
        Health -= _damageAmount;

        if (Health > 0)
        {
            StartCoroutine(IChangeColor());

            return;
        }

        //Entity has been killed..
        GameManager.Instance.AddKillToScore();
        ReturnToPool();

        AIManager.Instance?.UnRegisterAgent(this);
    }
    #endregion

    #region Virtuals
    protected virtual void IdleUpdate(float _time)
    {
        //Do small movements basically
        if (!isIdleMoving)
        {
            //Pick a spot to move too
            Vector3 _desiredPos = transform.position + new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));

            if (NavMesh.SamplePosition(_desiredPos, out NavMeshHit _hit, 1.0f, ~0))
            {
                TargetPosition = _hit.position;
                isIdleMoving = true;
                idleTime = 0;
            }
        }

        if(distanceToTargetPos < 1.5f 
            && isIdleMoving)
        {
            //start the idle countdown
            idleTime += _time;

            if(idleTime > 5.0f)
            {
                isIdleMoving = false;
            }
        }
    }

    protected virtual void AttackUpdate(float _time)
    {

    }

    protected virtual void FollowUpdate(float _time)
    {
        TargetPosition = lastSeenPosition.Value;

        if(distanceToTargetPos < 2.5f 
           && !sawPlayer)
        {
            navMeshAgent.isStopped = true;
            CurrentState = EnemyState.Idle;
        }
        else if(distanceToTargetPos < 2.5f
            && sawPlayer)
        {
            //Try Attacking??

        }
    }
    #endregion

    #region Updates
    public void OnEntityUpdate(float _deltaTime)
    {
        distanceToTargetPos = (TargetPosition - transform.position).magnitude;

        switch (CurrentState)
        {
            case EnemyState.Idle:
                IdleUpdate(_deltaTime);
                break;

            case EnemyState.Attacking:
                AttackUpdate(_deltaTime);
                break;

            case EnemyState.Follow:
                FollowUpdate(_deltaTime);
                break;
        }

        SetSpeedFromState(CurrentState);
    }

    public void OnEntityFixedUpdate(float _fixedDeltaTime)
    {
        if (CanSeePlayer())
        {
            CurrentState = EnemyState.Follow;
        }
    }
    #endregion

    private bool CanSeePlayer()
    {
        Ray _ray = new Ray(transform.position, (PlayerController.Instance.PlayerCamera.transform.position - transform.position).normalized);
        if (Physics.Raycast(_ray, out RaycastHit _hit, sightDistance, hitMask))
        {
            if (_hit.collider.CompareTag(GameConstants.Tags.player))
            {
                //hit the player...
                lastSeenPosition = _hit.collider.transform.position;

                sawPlayer = true;
                return true;
            }
        }

        sawPlayer = false;
        return false;
    }

    private IEnumerator IChangeColor()
    {
        meshRenderer.material = redFlash;
        yield return GameConstants.WaitTimers.waitForPointOne;
        meshRenderer.material = whiteDefault;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(TargetPosition, 0.1f);
    }

    private void SetSpeedFromState(EnemyState _state)
    {
        switch (_state)
        {
            case EnemyState.Idle:
                navMeshAgent.speed = 1;
                navMeshAgent.acceleration = 2;
                break;

            case EnemyState.Attacking:
            case EnemyState.Follow:
                navMeshAgent.speed = movementSpeed;
                navMeshAgent.acceleration = 8;
                break;
        }
    }
}

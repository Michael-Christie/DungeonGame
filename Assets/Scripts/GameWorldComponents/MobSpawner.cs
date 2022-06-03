using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MobSpawner : MonoBehaviour
{
    [SerializeField] private float spawnRadius;
    [SerializeField] private float spawnFrequency; //not used at the momement but this should be the frequency the room ties to spawn a mob in it...

    [SerializeField] private Vector2Int spawnAmount;

    [SerializeField] private bool requiresPlayerInArea;

    [SerializeField] private GameConstants.EnemyID spawnType;

    //
    public void SpawnEnemysInWorld()
    {
        int _amountToSpawn = Random.Range(spawnAmount.x, spawnAmount.y);

        for (int i = 0; i < _amountToSpawn; i++)
        {
            Spawn();
        }
    }

    [ContextMenu("SpawnEnemy")]
    private void Spawn()
    {
        Vector2 _rndPos = Random.insideUnitCircle * spawnRadius;
        Vector3 _position = new Vector3(transform.position.x + _rndPos.x, transform.position.y, transform.position.z + _rndPos.y);

        if (NavMesh.SamplePosition(_position, out NavMeshHit _hit, 25f, 1))
        {
            IPoolable _enemyObject = ObjectPooler.Instance.GetObject((int)spawnType);

            _enemyObject.SetPosition(transform, _hit.position);
        }
    }
}

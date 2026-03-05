using System.Collections.Generic;
using UnityEngine;
public abstract class EnemySpawner : MonoBehaviour
{
    [SerializeField] protected List<Enemy> enemiesToSpawn = new();
    [SerializeField] protected float spawnRange;
    [SerializeField] protected int maxEnemyCount;
    
    protected int enemyCount;

#if UNITY_EDITOR
    protected List<Enemy> enemiesSpawned = new();
    [SerializeField] protected bool debugging;
#endif
    protected abstract void Spawn();
    protected void SpawnEnemy()
    {
        // Set position
        Vector3 pos = transform.position;
        pos.x += Random.Range(-spawnRange, spawnRange);
        pos.z += Random.Range(-spawnRange, spawnRange);

        // Find closest available navmesh point
        UnityEngine.AI.NavMeshHit myNavHit;
        if (UnityEngine.AI.NavMesh.SamplePosition(pos, out myNavHit, 100, -1))
        {
            pos = myNavHit.position;
        }

        // Select a random enemy type to spawn
        Enemy enemyToSpawn = enemiesToSpawn[Random.Range(0, enemiesToSpawn.Count)];

        // Spawn the selected enemy at the picked location
        Enemy enemy = Instantiate(enemyToSpawn, pos, Quaternion.identity);

        // Rotation
        Vector3 rot = enemy.transform.localEulerAngles;
        rot.y += Random.Range(0, 360);
        enemy.transform.localEulerAngles = rot;

        // Setup
        enemy.LinkSpawner(this);
        enemyCount++;

#if UNITY_EDITOR
        enemiesSpawned.Add(enemy);
#endif
    }
    public void RemoveEnemy()
    {
        enemyCount--;
    }
}
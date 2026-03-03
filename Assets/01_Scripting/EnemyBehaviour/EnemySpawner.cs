using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy enemyToSpawn;
    [SerializeField] private int maxEnemyCount;
    [SerializeField] private float enemySpawnInterval;
    [Range(0f,1f)][SerializeField] private float enemySpawnChance;
    [SerializeField] private float spawnRange;
    private int enemyCount;
    private void Start()
    {
        if (canRespawn) InvokeRepeating(nameof(Spawn), 0, enemySpawnInterval);
        else for (int i = 0; i < maxEnemyCount; i++) Spawn();
    }
    private void Spawn()
    {
        if (enemyCount >= maxEnemyCount) return;

        else if (Random.Range(0f,1f) >= 1f - enemySpawnChance)
        {
            // Set position
            Vector3 pos = transform.position;
            pos.x += Random.Range(-spawnRange, spawnRange);
            pos.z += Random.Range(-spawnRange, spawnRange);

            // Find closest available navmesh point
            NavMeshHit myNavHit;
            if (NavMesh.SamplePosition(pos, out myNavHit, 100, -1))
            {
                pos = myNavHit.position;
            }

            // Select a random enemy type to spawn
            Enemy enemyToSpawn = enemiesToSpawn[Random.Range(0, enemiesToSpawn.Count - 1)];

            // Spawn the selected enemy at the picked location
            Enemy enemy = Instantiate(enemyToSpawn, pos, Quaternion.identity);
            enemy.LinkSpawner(this);
            enemyCount++;
        }
    }
    public void RemoveEnemy()
    {
        enemyCount--;
    }
}
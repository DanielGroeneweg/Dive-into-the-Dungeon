using UnityEngine;
using System.Collections.Generic;
public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<Enemy> enemiesToSpawn = new();
    [SerializeField] private float spawnRange;
    [SerializeField] private bool canRespawn;
    [SerializeField] private int maxEnemyCount;
    [SerializeField] private float enemySpawnInterval;
    [Range(0f,1f)][SerializeField] private float enemySpawnChance;
    private int enemyCount;
    private void Start()
    {
        if (canRespawn) InvokeRepeating(nameof(Spawn), enemySpawnInterval, enemySpawnInterval);
        else for (int i = 0; i < maxEnemyCount; i++) Spawn();
    }
    private void Spawn()
    {
        if (enemyCount >= maxEnemyCount) return;

        else if (Random.Range(0f,1f) >= 1f - enemySpawnChance)
        {
            Vector3 pos = transform.position;
            pos.x += Random.Range(-spawnRange, spawnRange);
            pos.z += Random.Range(-spawnRange, spawnRange);

            Enemy enemyToSpawn = enemiesToSpawn[Random.Range(0, enemiesToSpawn.Count - 1)];

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
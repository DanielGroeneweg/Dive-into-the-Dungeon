using UnityEngine;
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
        InvokeRepeating(nameof(Spawn), enemySpawnInterval, enemySpawnInterval);
    }
    private void Spawn()
    {
        if (enemyCount >= maxEnemyCount) return;

        else if (Random.Range(0f,1f) >= 1f - enemySpawnChance)
        {
            Vector3 pos = transform.position;
            pos.x += Random.Range(-spawnRange, spawnRange);
            pos.z += Random.Range(-spawnRange, spawnRange);

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
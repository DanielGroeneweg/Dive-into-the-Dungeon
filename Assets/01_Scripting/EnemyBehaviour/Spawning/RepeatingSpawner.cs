using UnityEngine;

public class RepeatingSpawner : EnemySpawner
{
    [SerializeField] private float enemySpawnInterval;
    [Range(0f, 1f)][SerializeField] private float enemySpawnChance;
    protected override void Start()
    {
        InvokeRepeating(nameof(Spawn), 0, enemySpawnInterval);
    }
    protected override void Spawn()
    {
        if (enemyCount >= maxEnemyCount) return;

        else if (Random.Range(0f, 1f) >= 1f - enemySpawnChance)
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
            enemy.LinkSpawner(this);
            enemyCount++;
        }
    }
}

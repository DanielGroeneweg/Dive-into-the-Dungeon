using UnityEngine;
using NaughtyAttributes;
using System.Collections.Generic;
public class SingleBurstSpawner : EnemySpawner
{
#if UNITY_EDITOR
    private List<Enemy> enemiesSpawned = new();
#endif
    protected override void Start()
    {
        #if UNITY_EDITOR
        if (debugging) return;
        #endif

        for (int i = 0; i < maxEnemyCount; i++) Spawn();
    }
    protected override void Spawn()
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

        #if UNITY_EDITOR
        enemiesSpawned.Add(enemy);
        #endif
    }
#if UNITY_EDITOR
    [Button("Spawn", EButtonEnableMode.Playmode)]
    private void DebugSpawn()
    {
        if (!debugging) return;
        for (int i = 0; i < maxEnemyCount; i++) Spawn();
    }

    [Button("DeleteEnemies", EButtonEnableMode.Playmode)]
    private void DebugDelete()
    {
        if (!debugging) return;
        for (int i = enemiesSpawned.Count - 1; i >= 0; i--)
        {
            Enemy enemy = enemiesSpawned[i];
            enemiesSpawned.RemoveAt(i);
            Destroy(enemy.gameObject);
        }
    }
#endif
}

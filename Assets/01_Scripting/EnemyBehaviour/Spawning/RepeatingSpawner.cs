using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

public class RepeatingSpawner : EnemySpawner
{
    [SerializeField] private float enemySpawnInterval;
    [Range(0f, 1f)][SerializeField] private float enemySpawnChance;
    private void Start()
    {
        #if UNITY_EDITOR
        if (debugging) return;
        #endif

        InvokeRepeating(nameof(Spawn), 0, enemySpawnInterval);
    }
    protected override void Spawn()
    {
        if (enemyCount >= maxEnemyCount) return;

        else if (Random.Range(0f, 1f) >= 1f - enemySpawnChance)
        {
            SpawnEnemy();
        }
    }
#if UNITY_EDITOR
    [Button("Spawn", EButtonEnableMode.Playmode)]
    private void DebugSpawn()
    {
        if (!debugging) return;
        Spawn();
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

using NaughtyAttributes;
public class SingleBurstSpawner : EnemySpawner
{
    private void Start()
    {
        #if UNITY_EDITOR
        if (debugging) return;
        #endif

        for (int i = 0; i < maxEnemyCount; i++) Spawn();
    }
    protected override void Spawn()
    {
        SpawnEnemy();
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

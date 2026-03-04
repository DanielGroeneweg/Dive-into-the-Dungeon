using System.Collections.Generic;
using UnityEngine;
public abstract class EnemySpawner : MonoBehaviour
{
    [SerializeField] protected List<Enemy> enemiesToSpawn = new();
    [SerializeField] protected float spawnRange;
    [SerializeField] protected int maxEnemyCount;
    
    protected int enemyCount;
    protected virtual void Start() { }
    protected abstract void Spawn();
    public void RemoveEnemy()
    {
        enemyCount--;
    }
}
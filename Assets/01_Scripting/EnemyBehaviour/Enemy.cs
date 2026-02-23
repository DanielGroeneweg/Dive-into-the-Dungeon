using UnityEngine;
using System.Collections.Generic;
public class Enemy : MonoBehaviour
{
    [SerializeField] private List<ItemDrop> itemDrops;
    [SerializeField] private EnemyTypes enemyType;
    [SerializeField] private float xpOnDeath;
    private EnemySpawner spawner;
    public void EnemyDeath()
    {
        EnemyDeathEventData data = new EnemyDeathEventData(enemyType, xpOnDeath, itemDrops);
        spawner.RemoveEnemy();
    }
    public void LinkSpawner(EnemySpawner spawner) { this.spawner = spawner; }
}
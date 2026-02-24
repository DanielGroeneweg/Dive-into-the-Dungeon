using UnityEngine;
using System.Collections.Generic;
public class Enemy : MonoBehaviour
{
    [SerializeField] private List<ItemDrop> itemDrops;
    [SerializeField] private EnemyTypes enemyType;
    [SerializeField] private float xpOnDeath;
    private EnemySpawner spawner;
    public void Killenemy()
    {
        EnemyDeathEventData data = new EnemyDeathEventData(enemyType, xpOnDeath, itemDrops);
        EventBusManager.Instance.EnemyDeathEvent.Raise(data);
    }
    public void RemoveEnemy()
    {
        if (spawner != null) spawner.RemoveEnemy();
    }
    public void LinkSpawner(EnemySpawner spawner) { this.spawner = spawner; }
}
using UnityEngine;
using System.Collections.Generic;
public class Enemy : MonoBehaviour
{
    [SerializeField] private List<ItemDrop> itemDrops;
    [SerializeField] private EnemyTypes enemyType;
    [SerializeField] private float xpOnDeath;
    private EnemySpawner spawner;
    public void KillEnemy()
    {
        EnemyDeathEventData data = new EnemyDeathEventData(enemyType, xpOnDeath);
        GameManager.Instance.EnemyDeath(data);

        foreach(ItemDrop itemDrop in itemDrops)
        {
            if (Random.Range(0f,0.99f) >= 1 - itemDrop.dropChance)
            {
                if (itemDrop.item != null)
                {
                    Debug.Log($"dropping {itemDrop.item}");
                    GameManager.Instance.GetItem(new GetItemEventData(itemDrop.item));
                }
            }
        }
    }
    public void RemoveEnemy()
    {
        if (spawner != null) spawner.RemoveEnemy();
    }
    public void LinkSpawner(EnemySpawner spawner) { this.spawner = spawner; }
}
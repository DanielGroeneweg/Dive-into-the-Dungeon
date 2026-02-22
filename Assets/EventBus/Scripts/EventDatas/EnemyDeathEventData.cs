using System.Collections.Generic;
using UnityEngine;
public class EnemyDeathEventData : GameEventData
{
    public EnemyTypes EnemyType { get; private set; }
    public float XPToGain { get; private set; }
    public Dictionary<Item, float> ItemDrops { get; private set; }
    public EnemyDeathEventData(EnemyTypes enemyType, float xp, Dictionary<Item, float> itemDrops)
    {
        EnemyType = enemyType;
        XPToGain = xp;
        ItemDrops = itemDrops;
    }
}
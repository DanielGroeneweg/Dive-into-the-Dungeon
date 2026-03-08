using System.Collections.Generic;
using UnityEngine;
public class EnemyDeathEventData : GameEventData
{
    public EnemyTypes enemyType { get; private set; }
    public float xpToGain { get; private set; }
    public EnemyDeathEventData(EnemyTypes enemyType, float xp)
    {
        this.enemyType = enemyType;
        xpToGain = xp;
    }
}
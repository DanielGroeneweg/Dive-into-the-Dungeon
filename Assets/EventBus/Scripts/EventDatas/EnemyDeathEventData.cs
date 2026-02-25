using System.Collections.Generic;
using UnityEngine;
public class EnemyDeathEventData : GameEventData
{
    public EnemyTypes EnemyType { get; private set; }
    public float XPToGain { get; private set; }
    public EnemyDeathEventData(EnemyTypes enemyType, float xp)
    {
        EnemyType = enemyType;
        XPToGain = xp;
    }
}
using System;
using UnityEngine;
[Serializable]
public class DefeatEnemyQuest : Quest
{
    [SerializeField] private EnemyTypes enemyType;
    public EnemyTypes EnemyType => enemyType;
    public override void ProgressQuest()
    {
        Progress++;
    }
}
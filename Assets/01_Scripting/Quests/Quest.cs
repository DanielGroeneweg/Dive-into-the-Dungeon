using System;
using UnityEngine;
[Serializable]
public abstract class Quest
{
    [SerializeField] private string questName;
    [SerializeField] protected float amountNeeded;
    [SerializeField] private QuestReward reward;
    public float Progress {  get; protected set; }
    public float AmountNeeded => amountNeeded;
    public string QuestName => questName;
    public QuestReward Reward => reward;
    public abstract void ProgressQuest();
}
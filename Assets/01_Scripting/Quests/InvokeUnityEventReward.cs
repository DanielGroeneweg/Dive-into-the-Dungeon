using UnityEngine;
using UnityEngine.Events;
public class InvokeUnityEventReward : QuestReward
{
    [SerializeField] private UnityEvent reward;
    public override void InvokeReward()
    {
        reward?.Invoke();
    }
}
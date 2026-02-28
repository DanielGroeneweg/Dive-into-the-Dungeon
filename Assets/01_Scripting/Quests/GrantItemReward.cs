using UnityEngine;
using System.Collections.Generic;
public class GrantItemReward : QuestReward
{
    [SerializeField] List<Item> items = new();
    public override void InvokeReward()
    {
        foreach (Item item in items)
        {
            EventBusManager.Instance.GetItemEvent.Raise(new GetItemEventData(item));
        }
    }
}
using UnityEngine;
public class GetItemEventData : GameEventData
{
    public Item item {  get; private set; }
    public GetItemEventData(Item item)
    {
        this.item = item;
    }
}
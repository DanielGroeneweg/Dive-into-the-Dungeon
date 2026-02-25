using UnityEngine;
public class GetItemEventData : GameEventData
{
    public Item Item {  get; private set; }
    public GetItemEventData(Item item)
    {
        Item = item;
    }
}
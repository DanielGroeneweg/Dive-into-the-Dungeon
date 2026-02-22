using UnityEngine;
public class LoseManaEventData : GameEventData
{
    public float mana {  get; private set; }
    public LoseManaEventData(float mana)
    {
        this.mana = mana;
    }
}

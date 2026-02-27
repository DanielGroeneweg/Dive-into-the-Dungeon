using UnityEngine;
public class GainManaEventData : GameEventData
{
    public float mana {  get; private set; }
    public GainManaEventData(float mana)
    {
        this.mana = mana;
    }
}
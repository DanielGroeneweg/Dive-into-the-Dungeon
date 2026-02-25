using System;
[Serializable]
public class SpellStats
{
    public float damage;
    public float duration;
    public float areaSize;
    public float force;
    public void CopyFrom(SpellStats other)
    {
        damage = other.damage;
        duration = other.duration;
        areaSize = other.areaSize;
        force = other.force;
    }
}
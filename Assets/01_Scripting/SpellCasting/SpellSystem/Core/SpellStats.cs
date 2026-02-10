using System;
using UnityEngine;
using NaughtyAttributes;
[Serializable]
public class SpellStats
{
    public float manaCost;
    public float damage;
    public float duration;
    public float areaSize;
    public void CopyFrom(SpellStats other)
    {
        damage = other.damage;
        manaCost = other.manaCost;
        duration = other.duration;
        areaSize = other.areaSize;
    }
}
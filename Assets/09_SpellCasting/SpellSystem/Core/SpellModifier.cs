using UnityEngine;
public abstract class SpellModifier : SpellComponent
{
    [SerializeField] protected float multiplier = 1.5f;
    public abstract void ApplyModification(SpellStats stats);
}
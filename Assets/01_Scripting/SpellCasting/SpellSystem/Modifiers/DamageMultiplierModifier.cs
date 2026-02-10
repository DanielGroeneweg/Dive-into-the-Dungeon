using UnityEngine;

[CreateAssetMenu(menuName = "Spells/Modifiers/Damage Multiplier")]
public class DamageMultiplierModifier : SpellModifier
{
    public float multiplier = 1.5f;
    public override void ApplyModification(SpellStats stats)
    {
        stats.damage *= multiplier;
        stats.manaCost *= multiplier;
    }
}
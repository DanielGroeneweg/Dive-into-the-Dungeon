using UnityEngine;

[CreateAssetMenu(menuName = "Spells/Modifiers/Damage Multiplier")]
public class DamageMultiplierModifier : SpellModifier
{
    public override void ApplyModification(SpellStats stats)
    {
        stats.damage *= multiplier;
    }
}
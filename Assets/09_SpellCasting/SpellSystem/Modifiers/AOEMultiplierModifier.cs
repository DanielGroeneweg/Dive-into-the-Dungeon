using UnityEngine;
[CreateAssetMenu(menuName = "Spells/Modifiers/AOE Multiplier")]
public class AOEMultiplierModifier : SpellModifier
{
    public override void ApplyModification(SpellStats stats)
    {
        stats.areaSize *= multiplier;
    }
}

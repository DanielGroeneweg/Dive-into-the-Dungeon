using UnityEngine;
[CreateAssetMenu(menuName = "Spells/Modifiers/AOE Multiplier")]
public class AOEMultiplierModifier : SpellModifier
{
    public float multiplier = 1.5f;
    public override void ApplyModification(SpellStats stats)
    {
        stats.areaSize *= multiplier;
    }
}

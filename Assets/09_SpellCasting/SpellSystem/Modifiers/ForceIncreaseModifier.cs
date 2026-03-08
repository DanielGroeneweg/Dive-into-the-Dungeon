using UnityEngine;
[CreateAssetMenu(menuName = "Spells/Modifiers/Force Increase")]
public class ForceIncreaseModifier : SpellModifier
{
    public override void ApplyModification(SpellStats stats)
    {
        stats.force *= multiplier;
    }
}

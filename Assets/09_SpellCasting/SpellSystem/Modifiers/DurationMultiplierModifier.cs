using UnityEngine;
[CreateAssetMenu(menuName = "Spells/Modifiers/Duration Multiplayer")]
public class DurationMultiplierModifier : SpellModifier
{
    public override void ApplyModification(SpellStats stats)
    {
        stats.duration *= multiplier;
    }
}

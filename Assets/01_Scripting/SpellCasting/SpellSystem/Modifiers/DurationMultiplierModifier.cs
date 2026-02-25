using UnityEngine;
[CreateAssetMenu(menuName = "Spells/Modifiers/Duration Multiplayer")]
public class DurationMultiplierModifier : SpellModifier
{
    public float multiplier = 1.5f;
    public override void ApplyModification(SpellStats stats)
    {
        stats.duration *= multiplier;
    }
}

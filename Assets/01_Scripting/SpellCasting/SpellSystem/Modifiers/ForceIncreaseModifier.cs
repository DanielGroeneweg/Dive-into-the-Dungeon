using UnityEngine;
[CreateAssetMenu(menuName = "Spells/Modifiers/Force Increase")]
public class ForceIncreaseModifier : SpellModifier
{
    [SerializeField] private float multiplier;
    public override void ApplyModification(SpellStats stats)
    {
        stats.force *= multiplier;
        stats.manaCost *= multiplier;
    }
}

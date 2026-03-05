using UnityEngine;
[CreateAssetMenu(menuName = "Spells/Effects/Damage Aura")]
public class DamageAuraEffect : SpellEffect
{
    [SerializeField] private float orbDistanceFromCenter;
    [SerializeField] private float numberOfOrbs;
    [SerializeField] private float orbMovementSpeed;
    [SerializeField] private SpellDamageAura auraPrefab;
    public override void Execute(SpellStats stats, SpellContext context)
    {
        SpellDamageAura aura = Instantiate(auraPrefab, context.spellPosition, context.spellRotation);
        aura.context = context;
        aura.stats = stats;
        aura.numberOfOrbs = numberOfOrbs;
        aura.orbMovementSpeed = orbMovementSpeed;
        aura.orbDistanceFromCenter = orbDistanceFromCenter;
    }
}
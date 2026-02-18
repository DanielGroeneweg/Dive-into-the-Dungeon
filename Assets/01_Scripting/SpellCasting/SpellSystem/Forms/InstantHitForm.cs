using UnityEngine;
[CreateAssetMenu(menuName = "Spells/Forms/Instant Hit")]
public class InstantHitForm : SpellForm
{
    [SerializeField] private SpellInstantHitParticles particlePrefab;
    public override void Execute(SpellContext context)
    {
        Debug.Log("Instant Hit");
        if (context.target == null) return;

        SpellInstantHitParticles particles = Instantiate(particlePrefab, context.spellPosition, context.spellRotation);
        particles.StartCountdown();
        foreach (ModifiedEffect effect in context.effects)
            effect.effect.Execute(effect.stats, context);
    }
}
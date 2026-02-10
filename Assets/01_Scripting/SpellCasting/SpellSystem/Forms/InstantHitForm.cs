using UnityEngine;
[CreateAssetMenu(menuName = "Spells/Forms/Instant Hit")]
public class InstantHitForm : SpellForm
{
    public override void Execute(SpellContext context)
    {
        context.spellRotation = context.target.transform.rotation;

        foreach (ModifiedEffect effect in context.effects)
            effect.effect.Execute(effect.stats, context);
    }
}
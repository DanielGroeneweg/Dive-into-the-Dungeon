using UnityEngine;
[CreateAssetMenu(menuName = "Spells/Forms/Target Self")]
public class TargetSelfForm : SpellForm
{
    public override void Execute(SpellContext context)
    {
        context.target = context.caster;
        context.spellPosition = context.caster.transform.position;
        context.spellRotation = Camera.main.transform.rotation;

        foreach (ModifiedEffect effect in context.effects)
            effect.effect.Execute(effect.stats, context);
    }
}
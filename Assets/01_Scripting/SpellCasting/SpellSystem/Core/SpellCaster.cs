using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class SpellCaster : MonoBehaviour
{
    public SpellDefinition currentSpell;
    public void TryCast(InputAction.CallbackContext inputContext)
    {
        // Prevent multiple events from new input system
        if (inputContext.phase != InputActionPhase.Performed) return;

        // Cancel if no hitbox is found
        Camera cam = Camera.main;
        if (!Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit))
            return;

        SpellContext context = new SpellContext
        {
            caster = gameObject,
            target = hit.collider.gameObject,
            effects = new List<ModifiedEffect>()
        };

        // Create a list of all effects with their modifiers
        CombineEffectsAndModifiers(context);

        // Cast the spell
        currentSpell.form.Execute(context);
    }
    /// <summary>
    /// Combines All effects and modifiers into a list of 'modified effects'. Each modifier is applied to the last found effect
    /// in the list of effects and modifiers.
    /// </summary>
    /// <param name="context"></param>
    private void CombineEffectsAndModifiers(SpellContext context)
    {
        if (currentSpell.components.Length > 0)
        {
            SpellEffect first = (SpellEffect)currentSpell.components[0];
            if (first == null) return;
            ModifiedEffect effect = new ModifiedEffect { effect = first, stats = new SpellStats() };
            effect.stats.CopyFrom(first.stats);

            for (int i = 1; i < currentSpell.components.Length; i++)
            {
                SpellComponent component = currentSpell.components[i];
                if (component == null) continue;

                if (component is SpellEffect spellEffect)
                {
                    // Add current effect to context list
                    context.effects.Add(effect);

                    // Start creating new effect for context list
                    effect = new ModifiedEffect { effect = spellEffect, stats = new SpellStats() };
                    effect.stats.CopyFrom(spellEffect.stats);
                }

                if (component is SpellModifier modifier)
                {
                    modifier.ApplyModification(effect.stats);
                }
            }

            context.effects.Add(effect);
        }
    }
}

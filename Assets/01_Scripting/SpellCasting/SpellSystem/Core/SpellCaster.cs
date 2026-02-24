using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class SpellCaster : MonoBehaviour
{
    public SpellDefinition currentSpell;
    [SerializeField] private PlayerStats playerStats;
    public void TryCast(InputAction.CallbackContext inputContext)
    {
        // Prevent multiple events from new input system
        if (inputContext.phase != InputActionPhase.Performed) return;

        // Create spell context
        SpellContext context = new SpellContext
        {
            target = null,
            spellPosition = Vector3.zero,
            spellRotation = Quaternion.identity
        };

        context.caster = gameObject;
        context.effects = new List<ModifiedEffect>();

        Camera cam = Camera.main;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit))
        {
            context.target = hit.collider.gameObject;
            context.spellPosition = hit.point;
            context.spellRotation = Quaternion.LookRotation(hit.normal);
        }

        // Create a list of all effects with their modifiers
        CombineEffectsAndModifiers(context);

        // Mana cost and checks happen here
        float manaCost = currentSpell.form.ManaCost;
        foreach (ModifiedEffect effect in context.effects) manaCost += effect.cost;

        // Do nothing if the player doesn't have enough mana
        if (!playerStats.HasEnoughMana(manaCost)) return;

        // Invoke mana loss event
        EventBusManager.Instance.LoseManaEvent.Raise(new LoseManaEventData(manaCost));

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
            // Get the first effect of the spell
            SpellEffect first = (SpellEffect)currentSpell.components[0];

            // Do nothing if no effects are attached
            if (first == null) return;

            // Create a modified effect to add to the context list
            ModifiedEffect modifiedEffect = new ModifiedEffect { effect = first, stats = new SpellStats() };
            modifiedEffect.stats.CopyFrom(first.stats);

            // Set mana cost
            modifiedEffect.cost = first.ManaCost;

            for (int i = 1; i < currentSpell.components.Length; i++)
            {
                // Casting
                SpellComponent component = currentSpell.components[i];

                // Skip if component is invalid
                if (component == null) continue;

                if (component is SpellEffect spellEffect)
                {
                    // Add current effect to context list
                    context.effects.Add(modifiedEffect);

                    // Start creating new effect for context list
                    modifiedEffect = new ModifiedEffect { effect = spellEffect, stats = new SpellStats() };
                    modifiedEffect.stats.CopyFrom(spellEffect.stats);

                    // Set mana cost
                    modifiedEffect.cost = spellEffect.ManaCost;
                }

                if (component is SpellModifier modifier)
                {
                    modifier.ApplyModification(modifiedEffect.stats);

                    // Add mana cost
                    modifiedEffect.cost += modifier.ManaCost;
                }
            }

            // Add the final effect
            context.effects.Add(modifiedEffect);
        }
    }
}
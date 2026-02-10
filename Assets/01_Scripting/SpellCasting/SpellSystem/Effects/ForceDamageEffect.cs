using UnityEngine;
[CreateAssetMenu(menuName = "Spells/Effects/Force Damage")]
public class ForceDamageEffect : SpellEffect
{
    public override void Execute(SpellStats stats, SpellContext context)
    {
        Health health = context.target.GetComponent<Health>();
        if (health != null)
        {
            health.TakeDamage(stats.damage);
            Debug.Log($"Spell hit {context.target.name} for {stats.damage} Damage!");
        }

        Debug.Log("Applied Force Damage Effect!");
    }
}
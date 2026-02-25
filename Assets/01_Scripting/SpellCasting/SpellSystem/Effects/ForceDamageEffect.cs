using UnityEngine;
[CreateAssetMenu(menuName = "Spells/Effects/Force Damage")]
public class ForceDamageEffect : SpellEffect
{
    public override void Execute(SpellStats stats, SpellContext context)
    {
        if (context.target.tag == "Player")
        {
            DamagePlayerEventData data = new DamagePlayerEventData(stats.damage, context.caster);
            EventBusManager.Instance.DamagePlayerEvent.Raise(data);
            Debug.Log($"Spell hit {context.target.name} for {stats.damage} Damage!");
        }

        else
        {
            Health health = context.target.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(stats.damage);
                Debug.Log($"Spell hit {context.target.name} for {stats.damage} Damage!");
            }
        }
    }
}
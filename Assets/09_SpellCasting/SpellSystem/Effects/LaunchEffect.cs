using UnityEngine;
[CreateAssetMenu(menuName = "Spells/Effects/Launch")]
public class LaunchEffect : SpellEffect
{
    public override void Execute(SpellStats stats, SpellContext context)
    {
        Rigidbody rb = context.target.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Vector3 direction = context.spellRotation * Vector3.forward;
            rb.AddForce(direction.normalized * stats.force, ForceMode.Impulse);
        }
    }
}
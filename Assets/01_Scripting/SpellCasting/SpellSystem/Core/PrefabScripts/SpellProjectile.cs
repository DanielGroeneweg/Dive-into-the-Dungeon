using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class SpellProjectile : MonoBehaviour
{
    public SpellContext context;
    public float projectileSpeed = 1f;
    public float maxSpeed = 10f;
    public Rigidbody rb;
    private void FixedUpdate()
    {
        if (rb.angularVelocity.magnitude < maxSpeed) rb.AddForce(transform.forward * projectileSpeed, ForceMode.VelocityChange);
    }
    private void OnTriggerEnter(Collider collider)
    {
        context.spellPosition = transform.position;
        context.spellRotation = transform.rotation;

        foreach (ModifiedEffect effect in context.effects)
            effect.effect.Execute(effect.stats, context);
        Destroy(gameObject);
    }
}
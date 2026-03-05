using UnityEngine;
using System.Collections;
[RequireComponent(typeof(Rigidbody))]
public class SpellProjectile : MonoBehaviour
{
    public SpellContext context;
    [SerializeField] private float projectileSpeed = 1f;
    [SerializeField] private float maxSpeed = 10f;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float lifeTime = 60;
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
    public void StartCountdown()
    {
        StartCoroutine(Countdown());
    }
    private IEnumerator Countdown()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }
}
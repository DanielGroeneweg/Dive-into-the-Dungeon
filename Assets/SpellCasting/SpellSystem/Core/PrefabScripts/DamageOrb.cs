using UnityEngine;
public class DamageOrb : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    public SpellDamageAura parent;
    private void OnTriggerEnter(Collider other)
    {
        if (other != null) parent.OrbHitCollider(other);
    }
}
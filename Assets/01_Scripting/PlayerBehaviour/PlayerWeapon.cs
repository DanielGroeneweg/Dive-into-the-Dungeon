using UnityEngine;
public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] private Collider weaponCollider;
    public Collider WeaponCollider => weaponCollider;
    private void OnTriggerEnter(Collider other)
    {
        Health health = other.GetComponent<Health>();

        if (health != null)
        {
            float dmg = Locator.instance.Inventory.equippedItems.weapon.Damage;
            health.TakeDamage(dmg);
        }
    }
}
using UnityEngine;
public class DamagePlayerEventData
{
    public GameObject damageSource { get; private set; }
    public float damage { get; private set; }

    public DamagePlayerEventData(GameObject damageSource, float damage)
    {
        this.damageSource = damageSource;
        this.damage = damage;
    }
}
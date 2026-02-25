using UnityEngine;
public class DamagePlayerEventData : GameEventData
{
    public GameObject damageSource { get; private set; }
    public float damage { get; private set; }
    public DamagePlayerEventData(float damage, GameObject source)
    {
        this.damageSource = damageSource;
        this.damage = damage;
    }
}
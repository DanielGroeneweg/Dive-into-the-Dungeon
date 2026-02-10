using UnityEngine;
[CreateAssetMenu(menuName = "Spells/Forms/Projectile")]
public class ProjectileForm : SpellForm
{
    public SpellProjectile prefab;
    public override void Execute(SpellContext context)
    {
        SpellProjectile projectile = Instantiate(prefab, Camera.main.transform.position + Camera.main.transform.forward, Camera.main.transform.rotation);
        projectile.context = context;
    }
}

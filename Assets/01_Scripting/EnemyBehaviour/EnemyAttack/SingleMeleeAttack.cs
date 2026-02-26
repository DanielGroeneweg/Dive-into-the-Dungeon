using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class SingleMeleeAttack : Attack
{
    [SerializeField] private List<Collider> hitboxes = new();
    [SerializeField] private AnimationClip attackAnimation;
    private void Start()
    {
        foreach (Collider hitbox in hitboxes) hitbox.enabled = false;
    }
    public override void DoAttack()
    {
        Vector3 target = Locator.instance.Player.position;
        target.y = transform.position.y;
        transform.LookAt(target);

        animator.SetTrigger("Attack");
        foreach (Collider hitbox in hitboxes) hitbox.enabled = true;
        StartCoroutine(EndAttack());
    }
    public void PlayerHit()
    {
        DamagePlayerEventData data = new DamagePlayerEventData(damage, gameObject);
        EventBusManager.Instance.DamagePlayerEvent.Raise(data);
    }
    private IEnumerator EndAttack()
    {
        yield return null;
        yield return new WaitForSeconds(attackAnimation.length);
        foreach (Collider hitbox in hitboxes) hitbox.enabled = false;
        AttackEndAction.Invoke();
    }
}
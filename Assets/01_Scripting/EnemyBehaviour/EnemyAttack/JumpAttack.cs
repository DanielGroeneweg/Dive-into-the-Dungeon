using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
public class JumpAttack : Attack
{
    [SerializeField] private float jumpHeight;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private float chargeTime;
    [SerializeField] private float slamTime;
    [SerializeField] private float slamRadius;
    public override void DoAttack()
    {
        animator.SetFloat("MoveSpeed", 0);
        StartCoroutine(nameof(Jump));
    }
    private IEnumerator Jump()
    {
        Vector3 startPos = transform.position;
        while (transform.position.y < startPos.y + jumpHeight)
        {
            yield return null;
            Vector3 pos = transform.position;
            pos.y += (jumpHeight / jumpSpeed) * Time.deltaTime;
            transform.position = pos;
        }
        StartCoroutine(nameof(Charge));
    }
    private IEnumerator Charge()
    {
        float time = 0;
        while (time < chargeTime)
        {
            yield return null;
            time += Time.deltaTime;
            Vector3 pos = Locator.instance.Player.position;
            pos.y = transform.position.y;
            transform.LookAt(pos);
        }
        StartCoroutine(nameof(Slam));
    }
    private IEnumerator Slam()
    {
        Vector3 startPos = transform.position;
        Vector3 targetPos = Locator.instance.Player.position;
        targetPos.y -= 1.04f;

        // Find a suitable landing spot
        Vector3 target = Vector3.zero;
        foreach(RaycastHit hit in Physics.RaycastAll(startPos, targetPos - startPos))
        {
            if (hit.collider.tag == "Ground")
            {
                target = hit.point;
                break;
            }
        }

        // Dash to position
        if (target != Vector3.zero)
        {
            Vector3 perSecond = (target - startPos) / slamTime;
            float passedTime = 0;
            while (transform.position != target)
            {
                passedTime += Time.deltaTime;
                float time = passedTime <= slamTime? Time.deltaTime : Time.deltaTime - (passedTime - slamTime);
                transform.position += perSecond * time;
                yield return null;
            }
        }

        // Fall down
        else
        {
            while (transform.position.y > startPos.y - jumpHeight)
            {
                yield return null;
                Vector3 pos = transform.position;
                pos.y += (jumpHeight / slamTime) * Time.deltaTime;
                transform.position = pos;
            }
        }

        // Damage player if player is near
        foreach (Collider collider in Physics.OverlapSphere(transform.position, slamRadius))
        {
            if (collider.tag == "Player")
            {
                DamagePlayerEventData data = new DamagePlayerEventData(damage, gameObject);
                EventBusManager.instance.DamagePlayerEvent.Raise(data);
                break;
            }
        }
    }
}
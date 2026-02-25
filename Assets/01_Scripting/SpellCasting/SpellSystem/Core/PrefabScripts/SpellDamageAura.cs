using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
public class SpellDamageAura : MonoBehaviour
{
    [SerializeField] private DamageOrb damageOrbPrefab;
    [HideInInspector] public SpellContext context;
    [HideInInspector] public SpellStats stats;
    [HideInInspector] public float numberOfOrbs;
    [HideInInspector] public float orbMovementSpeed;
    [HideInInspector] public float orbDistanceFromCenter;
    private Transform followTarget;
    private Vector3 oldTargetPos;
    public void OrbHitCollider(Collider collider)
    {
        if (collider.tag == "Player")
        {
            DamagePlayerEventData data = new DamagePlayerEventData(stats.damage, context.caster);
            EventBusManager.Instance.DamagePlayerEvent.Raise(data);
            Debug.Log($"Spell hit {context.target.name} for {stats.damage} Damage!");
        }

        else
        {

            Health health = collider.gameObject.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(stats.damage);
                Debug.Log($"Spell hit {collider.gameObject.name} for {stats.damage} Damage!");
            }
        }
    }
    private void LateUpdate()
    {
        if (followTarget == null) return;

        if (followTarget.position != oldTargetPos)
        {
            transform.position += followTarget.position - oldTargetPos;
            oldTargetPos = followTarget.position;
        }
    }
    private void Update()
    {
        transform.Rotate(Vector3.up, orbMovementSpeed * Time.deltaTime, Space.World);
    }
    private void Start()
    {
        Debug.Log("Creating orbs!");
        for (int i = 0; i < numberOfOrbs; i++)
        {
            DamageOrb orb = (Instantiate(damageOrbPrefab, transform.position, Quaternion.identity));
            orb.transform.parent = transform;
            orb.transform.localPosition = new Vector3(orbDistanceFromCenter * stats.areaSize, 0, 0);
            orb.transform.RotateAround(transform.position, new Vector3(0, 1, 0), 360f / numberOfOrbs * i);
            orb.parent = this;
        }

        StartCoroutine(DestroyAfterDuration(stats.duration));

        followTarget = context.target.transform;
        oldTargetPos = followTarget.position;
    }
    private IEnumerator DestroyAfterDuration(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
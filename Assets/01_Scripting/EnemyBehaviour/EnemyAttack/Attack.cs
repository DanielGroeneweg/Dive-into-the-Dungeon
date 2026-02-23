using UnityEngine;
using System;
public abstract class Attack : MonoBehaviour
{
    [SerializeField] protected float attackRange = 1;
    [SerializeField] protected float damage = 25;
    [SerializeField] protected Animator animator;
    public Action action;
    public float AttackRange => attackRange;
    public float Damage => damage;
    public abstract void DoAttack();
}

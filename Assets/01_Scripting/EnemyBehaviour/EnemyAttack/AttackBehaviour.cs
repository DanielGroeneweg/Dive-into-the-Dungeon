using System.Collections.Generic;
using UnityEngine;

public class AttackBehaviour : MonoBehaviour
{
    [SerializeField] private List<Attack> attacks = new();
    private bool canAttack;
    public bool CanAttack => canAttack;
    private bool isAttacking;
    private int index = 0;
    private void Update()
    {
        if (isAttacking) return;

        if ((transform.position - Locator.instance.Player.position).magnitude <= attacks[index].AttackRange) canAttack = true;
        else canAttack = false;
    }
    public void DoAttack()
    {
        if (!isAttacking)
        {
            attacks[index].DoAttack();
            attacks[index].AttackEndAction = SetAttackingFalse;
            isAttacking = true;
            index = index + 1 >= attacks.Count ? 0 : index + 1;
            Debug.Log(index);
        }
    }
    private void SetAttackingFalse() { isAttacking = false; }
}
using NaughtyAttributes;
using UnityEngine;
public abstract class MoveBehaviour : MonoBehaviour
{
    [SerializeField] protected float movementSpeed = 10;
    [SerializeField] protected bool hasDetectionRange = true;
    [ShowIf("hasDetectionRange")]
    [SerializeField] protected float detectionRange = 5;
    [SerializeField] private Animator animator;
    public void DoMovement()
    {
        if (Locator.instance.Player == null)
        {
            Debug.LogError($"Player Reference not set for {gameObject.name}");
            return;
        }

        if (hasDetectionRange)
        {
            if ((Locator.instance.Player.transform.position - transform.position).magnitude <= detectionRange)
            {
                Move();
                animator.SetFloat("MoveSpeed", 1);
            }
            else StopMoving();
        }

        else
        {
            Move();
            animator.SetFloat("MoveSpeed", 1);
        }
    }
    public void StopMovement()
    {
        StopMoving();
        animator.SetFloat("MoveSpeed", 0);
    }
    protected abstract void Move();
    protected abstract void StopMoving();
}

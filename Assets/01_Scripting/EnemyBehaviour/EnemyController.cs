using UnityEngine;
[RequireComponent(typeof(MoveBehaviour))]
[RequireComponent(typeof(AttackBehaviour))]
public class EnemyController : MonoBehaviour
{
    [SerializeField] private MoveBehaviour moveBehaviour;
    [SerializeField] private AttackBehaviour attackBehaviour;
    private void FixedUpdate()
    {
        if (!attackBehaviour.CanAttack) moveBehaviour.DoMovement();

        else
        {
            moveBehaviour.StopMovement();
            attackBehaviour.DoAttack();
        }
    }
}
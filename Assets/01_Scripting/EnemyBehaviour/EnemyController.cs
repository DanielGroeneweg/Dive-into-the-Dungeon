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
    private void OnEnable()
    {
        GameManager.Instance.LinkGameOverEvent(Disable);
    }
    private void OnDisable()
    {
        GameManager.Instance.UnlinkGameOverEvent(Disable);
    }
    private void Disable(GameOverEventData data) { enabled = false; }
}
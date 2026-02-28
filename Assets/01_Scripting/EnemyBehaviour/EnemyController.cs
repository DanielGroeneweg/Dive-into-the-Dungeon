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
        EventBusManager.Instance.GameOverEvent.Register(Disable);
    }
    private void OnDisable()
    {
        EventBusManager.Instance.GameOverEvent.Unregister(Disable);
    }
    private void Disable(GameOverEventData data) { enabled = false; }
}
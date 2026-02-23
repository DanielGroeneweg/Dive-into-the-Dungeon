using UnityEngine;
[RequireComponent(typeof(MoveBehaviour))]
[RequireComponent(typeof(AttackBehaviour))]
public class BossController : MonoBehaviour
{
    [SerializeField] private MoveBehaviour moveBehaviour;
    [SerializeField] private AttackBehaviour attackBehaviour;
}
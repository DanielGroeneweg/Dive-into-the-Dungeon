using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(NavMeshAgent))]
public class WalkingNavMeshAgent : MoveBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    private void Start()
    {
        agent.speed = movementSpeed;
    }
    protected override void Move()
    {
        agent.SetDestination(Locator.instance.Player.position);
    }
    protected override void StopMoving()
    {
        agent.isStopped = true;
    }
}
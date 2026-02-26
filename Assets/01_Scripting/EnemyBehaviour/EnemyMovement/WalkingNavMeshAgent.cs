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
        agent.enabled = true;
        Vector3 destination = Locator.instance.Player.position;
        destination.y -= 1;
        agent.SetDestination(destination);
        agent.isStopped = false;
    }
    protected override void StopMoving()
    {
        if (!agent.isActiveAndEnabled) return;

        agent.isStopped = true;
        agent.enabled = false;
    }
}
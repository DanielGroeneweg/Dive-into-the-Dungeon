using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class Walking : MoveBehaviour
{
    [SerializeField] private Rigidbody rb;
    protected override void Move()
    {
        RotateToPlayer();
        MoveToPlayer();
    }
    private void RotateToPlayer()
    {
        Vector3 lookPos = player.transform.position;
        lookPos.y = transform.position.y;

        transform.LookAt(lookPos);

    }
    private void MoveToPlayer()
    {
        rb.AddForce(transform.forward * movementSpeed);
    }
}
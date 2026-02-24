using UnityEngine;
public class FlyThroughObjects : MoveBehaviour
{
    protected override void Move()
    {
        // Look at player
        transform.LookAt(Locator.instance.Player.position);

        // This method is called form fixed update, no need to use time.deltatime
        transform.position += transform.forward * movementSpeed;
    }
    protected override void StopMoving()
    {
        // No need to do anything here
    }
}
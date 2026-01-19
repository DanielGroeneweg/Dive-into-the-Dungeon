using NaughtyAttributes;
using UnityEngine;
public abstract class MoveBehaviour : MonoBehaviour
{
    [SerializeField] protected float movementSpeed = 10;
    [SerializeField] protected bool hasDetectionRange = true;
    [ShowIf("hasDetectionRange")]
    [SerializeField] protected float detectionRange = 5;

    protected PlayerController player;

    protected void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    protected void FixedUpdate()
    {
        if (player == null)
        {
            Debug.LogError($"Player Reference not set for {gameObject.name}");
            return;
        }

        if (hasDetectionRange)
        {
            if ((player.transform.position - transform.position).magnitude <= detectionRange) Move();
        }

        else
        {
            Move();
        }
    }

    protected abstract void Move();
}

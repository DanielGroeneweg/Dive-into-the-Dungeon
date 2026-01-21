using UnityEngine;
using NaughtyAttributes;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private Animator animator;
    private void Update()
    {
        if (Grounded() && animator.GetCurrentAnimatorStateInfo(0).IsName("Falling")) animator.Play("Idle");
        else if (!Grounded()) animator.Play("Falling");
    }

    private void FixedUpdate()
    {
        DoMovement();
    }

    #region Movement
    [Header("Movement")]
    [SerializeField] private float movementSpeed = 10f;
    [SerializeField] private float maxMovementSpeed = 10;
    [SerializeField] private float jumpForce = 5;
    [SerializeField] private float groundedRange = 1f;
    private Vector2 movement;
    private void DoMovement()
    {
        Walking();
    }
    private bool Grounded()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, groundedRange))
        {
            if (hit.collider.tag == "Ground") return true;
        }
        return false;
    }
    private void Walking()
    {
        if (rb.linearVelocity.magnitude < maxMovementSpeed)
        {
            rb.AddForce(transform.forward * movement.y * movementSpeed);
            rb.AddForce(transform.right * movement.x * movementSpeed);
        }
    }
    private void Jumping()
    {
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    public void OnMove(InputValue input)
    {
        movement = input.Get<Vector2>();
    }
    public void OnRoll(InputValue input)
    {
        animator.Play("Rolling");
    }
    #endregion

    #region Camera Control
    [Header("Camera")]
    [SerializeField] private Cameras cameraType = Cameras.ThirdPerson;
    [SerializeField] private bool cameraLocked = false;
    public void SetFirstPersonCamera()
    {
        cameraType = Cameras.FirstPerson;
    }
    public void SetThirdPersonCamera()
    {
        cameraType = Cameras.ThirdPerson;
    }
    public void LockCamera()
    {
        cameraLocked = !cameraLocked;
    }
    #endregion
}
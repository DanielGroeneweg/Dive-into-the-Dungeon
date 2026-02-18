using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
public class SimplePlayerController : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float maxMoveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float groundedRange;
    [SerializeField] private float mouseSensitivity;

    [Header("References")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private PlayerInput input;
    [SerializeField] private Camera playerCam;

    private Vector2 move;
    private Vector2 look;
    float xRotation = 0f;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void OnMove(InputValue input)
    {
        move = input.Get<Vector2>();
    }
    public void OnLook(InputValue input)
    {
        look = input.Get<Vector2>();
    }
    public void OnJump(InputValue input)
    {
        if (Grounded()) rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    private void FixedUpdate()
    {
        DoMovement();

        DoLookAround();
    }
    private void DoMovement()
    {
        if (rb.angularVelocity.magnitude >= maxMoveSpeed) return;

        rb.AddForce(transform.right * move.x * moveSpeed);
        rb.AddForce(transform.forward * move.y * moveSpeed);
    }
    private void DoLookAround()
    {
        // Rotate player on Y axis
        Vector3 rot = transform.localEulerAngles;
        rot.y += look.x * mouseSensitivity;
        transform.localEulerAngles = rot;

        // Rotate camera
        float mouseY = look.y * mouseSensitivity;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        playerCam.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
    private bool Grounded()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, out hit, groundedRange))
        {
            if (hit.collider.tag == "Ground") return true;
        }
        return false;
    }
}
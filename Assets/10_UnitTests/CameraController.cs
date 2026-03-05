using UnityEngine;
using UnityEngine.InputSystem;
public class CameraController : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float mouseSensitivity;
    private Vector2 move;
    private Vector2 look;
    private float xRotation;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        xRotation = transform.localEulerAngles.x;
    }
    private void Update()
    {
        DoLookAround();
    }
    private void FixedUpdate()
    {
        DoMovement();
    }
    private void DoMovement()
    {
        Vector3 movement = transform.forward * move.y + transform.right * move.x;
        transform.position += movement * moveSpeed;
    }
    private void DoLookAround()
    {
        Vector3 rot = transform.localEulerAngles;

        // Horizontal rotation
        rot.y += look.x * mouseSensitivity;

        // Vertical rotation
        xRotation -= look.y * mouseSensitivity;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, rot.y, 0f);
    }
    public void OnMove(InputValue input)
    {
        move = input.Get<Vector2>();
    }
    public void OnLook(InputValue input)
    {
        look = input.Get<Vector2>();
    }
}
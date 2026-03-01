using System.Collections;
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
    [SerializeField] private Animator attackAnimator;

    private Vector2 move;
    private Vector2 look;
    private float xRotation = 0f;
    private bool isAttacking = false;
    private Collider weaponCollider;
    private PlayerWeapon weapon;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void OnEnable()
    {
        GameManager.Instance.LinkEquipWeaponEvent(ChangeWeapon);
    }
    private void OnDisable()
    {
        GameManager.Instance.UnlinkEquipWeaponEvent(ChangeWeapon);
    }
    #region PlayerInput
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
        if (enabled && Grounded()) rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    public void OnAttack(InputValue input)
    {
        if (isAttacking || weapon == null || !enabled) return;

        isAttacking = true;
        weaponCollider.enabled = true;
        attackAnimator.speed = Locator.instance.Inventory.equippedItems.weapon.AttackSpeed;
        attackAnimator.SetTrigger("Attack");
        StartCoroutine(DisableAttack(Locator.instance.Inventory.equippedItems.weapon.AttackSpeed));
    }
    #endregion
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
    /// <summary>
    /// Returns if the player is grounded or not
    /// </summary>
    /// <returns></returns>
    private bool Grounded()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, out hit, groundedRange))
        {
            if (hit.collider.tag == "Ground") return true;
        }
        return false;
    }
    private IEnumerator DisableAttack(float attackDuration)
    {
        yield return new WaitForSeconds(attackDuration);
        weaponCollider.enabled = false;
        isAttacking = false;
    }
    private void ChangeWeapon(EquipWeaponEventData data)
    {
        if (weapon != null) Destroy(weapon.gameObject);

        if (data.weapon == null)
        {
            weapon = null;
            return;
        }

        weapon = Instantiate(data.weapon.Prefab, attackAnimator.transform.position, Quaternion.identity, attackAnimator.transform);
        weaponCollider = weapon.WeaponCollider;
        weaponCollider.enabled = false;
    }
}
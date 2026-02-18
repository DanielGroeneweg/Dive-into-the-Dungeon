using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class HudController : MonoBehaviour
{
    [SerializeField] private GameObject wheel;
    [SerializeField] private Image cursor;
    public void OnSpellWheel(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            wheel.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            cursor.enabled = false;
        }
        else if (ctx.canceled)
        {
            wheel.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            cursor.enabled = true;
        }
    }
}
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;
using UnityEngine.Events;
public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject inventoryUI;
    [SerializeField] private GameObject spellCraftingUI;
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private UnityEvent closeInventory;
    [SerializeField] private UnityEvent closeSpellCrafting;
    [SerializeField] private UnityEvent closePause;
    [SerializeField] private UnityEvent openPause;
    public void PressedEscape(CallbackContext context)
    {
        if (context.phase != UnityEngine.InputSystem.InputActionPhase.Performed) return;

        // Close current menu
        if (inventoryUI.activeSelf) closeInventory?.Invoke();
        else if (spellCraftingUI.activeSelf) closeSpellCrafting?.Invoke();
        else if (pauseMenuUI.activeSelf)
        {
            closePause?.Invoke();
            Cursor.lockState = CursorLockMode.Locked;
        }

        // If no menu is opened, open pause menu
        else
        {
            openPause?.Invoke();
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
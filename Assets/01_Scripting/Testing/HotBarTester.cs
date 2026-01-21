using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.InputSystem;
using System.Collections;
[Serializable]
public class HotBarSlot
{
    public Slider slider;
    public float cooldownTime;
    public bool canCast = true;
}
public class HotBarTester : MonoBehaviour
{
    [SerializeField] private HotBarSlot[] slots = new HotBarSlot[8];
    [SerializeField] private PlayerInput input;
    private void Start()
    {
        foreach(HotBarSlot slot in slots)
        {
            slot.slider.value = 0;
            slot.canCast = true;
        }
    }
    public void OnCastSpell(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;

        Debug.Log("Casting!");

        if (context.control is UnityEngine.InputSystem.Controls.KeyControl key)
        {
            int spellIndex = key.keyCode switch
            {
                Key.Digit1 => 0,
                Key.Digit2 => 1,
                Key.Digit3 => 2,
                Key.Digit4 => 3,
                Key.Digit5 => 4,
                Key.Digit6 => 5,
                Key.Digit7 => 6,
                Key.Digit8 => 7,
                _ => -1
            };

            if (spellIndex >= 0)
                CastSpell(spellIndex);
        }
    }
    private void CastSpell(int spellIndex)
    {
        if (slots[spellIndex].canCast)
        {
            slots[spellIndex].canCast = false;
            StartCoroutine(SpellCooldown(slots[spellIndex]));
        }
    }
    private IEnumerator SpellCooldown(HotBarSlot slot)
    {
        slot.slider.maxValue = slot.cooldownTime;
        slot.slider.value = slot.cooldownTime;
        while (slot.slider.value > 0)
        {
            yield return null;
            slot.slider.value -= Time.deltaTime;
        }

        slot.canCast = true;
    }
}
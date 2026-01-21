using System;
using System.Collections;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static UnityEditor.PlayerSettings;
public class SpellsHandling : MonoBehaviour
{
    [Serializable] private class HotBarSlider
    {
        public Image sliderImage;
        public Image backgroundImage;
        public Slider slider;
    }

    [Header("References")]
    [SerializeField] private PlayerStats stats;
    [SerializeField] private Inventory inventory;
    [SerializeField] private PlayerStatsDisplay statDisplay;
    [SerializeField] private HotBarSlider[] slots = new HotBarSlider[8];
    private void Start()
    {
        for (int i = 0; i <= 7; i++)
        {
            Spell spell = inventory.Spells[i];
            HotBarSlider slot = slots[i];
            if (inventory.Spells[i] == null)
            {
                slot.sliderImage.sprite = null;
                slot.backgroundImage.sprite = null;
            }

            else
            {
                slot.sliderImage.sprite = spell.Icon;
                slot.backgroundImage.sprite = spell.Icon;
                slot.slider.value = 0;
            }
        }
    }

    public void OnCastSpell(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;

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
        Spell spell = inventory.Spells[spellIndex];
        HotBarSlider slot = slots[spellIndex];

        if (spell == null || !spell.CanCast || stats.mana < spell.ManaCost) return;

        Debug.Log($"Casting {spell.Name}");
        UpdateStats(spell);
        StartCoroutine(SpellCooldown(slot, spell));
    }
    private void UpdateStats(Spell spell)
    {
        stats.mana -= spell.ManaCost;
        statDisplay.UpdateMana(0, stats.maxMana, stats.mana);
    }
    private IEnumerator SpellCooldown(HotBarSlider slot, Spell spell)
    {
        slot.slider.maxValue = spell.Cooldown;
        slot.slider.value = spell.Cooldown;
        while (slot.slider.value > 0)
        {
            yield return null;
            slot.slider.value -= Time.deltaTime;
        }

        spell.CanCast = true;
    }
#if UNITY_EDITOR
    public void OnAttack(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;

        stats.mana = Mathf.Clamp(stats.mana + 10, 0, stats.maxMana);

        statDisplay.UpdateMana(0, stats.maxMana, stats.mana);
    }
#endif
}
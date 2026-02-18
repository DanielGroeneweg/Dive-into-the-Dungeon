using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class SpellSelector : MonoBehaviour
{
    [SerializeField] private SpellCaster caster;
    [SerializeField] private Color baseColor;
    [SerializeField] private Color selectedColor;
    [SerializeField] private TMP_Text spellNameText;
    [Serializable] public class SpellSlot
    {
        public Image slotBackground;
        public SpellDefinition spell;
        public Image spellIcon;
    }
    [SerializeField] private List<SpellSlot> spellSlots = new List<SpellSlot>();
    private void OnEnable()
    {
        for (int i = 0; i < spellSlots.Count; i++)
        {
            SpellSlot slot = spellSlots[i];
            if (caster.currentSpell == slot.spell)
            {
                slot.slotBackground.color = selectedColor;
                spellNameText.text = slot.spell.spellName;
            }

            else slot.slotBackground.color = baseColor;

            slot.slotBackground.alphaHitTestMinimumThreshold = 0.01f;
            
            if (slot.spell.components[0] != null)
            {
                slot.spellIcon.sprite = slot.spell.components[0].Icon;
                Color col = slot.spellIcon.color;
                col.a = 255;
                slot.spellIcon.color = col;
            }

            else
            {
                slot.spellIcon.sprite = null;
                Color col = slot.spellIcon.color;
                col.a = 0;
                slot.spellIcon.color = col;
            }
        }
    }
    public void SelectSpell(int index)
    {
        for (int i = 0; i < spellSlots.Count; i++)
        {
            SpellSlot slot = spellSlots[i];
            slot.slotBackground.color = (i == index) ? selectedColor : baseColor;
        }

        caster.currentSpell = spellSlots[index].spell;
        spellNameText.text = spellSlots[index].spell.spellName;
    }
}
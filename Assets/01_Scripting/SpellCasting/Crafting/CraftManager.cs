using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using TMPro;
public class CraftManager : MonoBehaviour
{
    [Tooltip("The Slot of the spell")]
    [SerializeField] private SpellSlot[] spellSlots = new SpellSlot[10];
    [SerializeField] private TMP_InputField nameInput;
    [Tooltip("The slots for the components of the spell")]
    [SerializeField] private Image[] componentSlots = new Image[10];
    [SerializeField] private Color selectedColor;
    [SerializeField] private Color unselectedColor;
    [SerializeField] private List<SpellComponentButton> components = new List<SpellComponentButton>();

    [SerializeField] private SpellSlot selected;
    private int selectedComponentIndex;
    /// <summary>
    /// Attempt to craft the spell, if it's invalid, clear
    /// </summary>
    public void Craft()
    {
        bool isValid = true;

        if (selected.spell.form == null) isValid = false;

        else
        {
            SpellComponent lastComponent = selected.spell.form;

            foreach (SpellComponent component in selected.spell.components)
            {
                if (lastComponent is SpellForm form)
                {
                    if (!form.allowedFollowUps.Contains(component))
                    {
                        isValid = false;
                        break;
                    } 
                }

                else if (lastComponent is SpellEffect effect)
                {
                    if (!effect.allowedFollowUps.Contains(component))
                    {
                        isValid = false;
                        break;
                    }
                }

                if (!(lastComponent is SpellModifier mod))
                {
                    lastComponent = component;
                }
            }
        }

        if (!isValid) Clear();
    }
    public void Clear()
    {
        foreach(Image image in componentSlots)
        {
            image.sprite = null;
        }

        for (int i = 0; i < selected.spell.components.Length; i++)
        {
            selected.spell.components[i] = null;
        }

        selected.spell.form = null;
    }
    /// <summary>
    /// Sets the given spell as the spell being edited
    /// </summary>
    /// <param name="selectedSlot"></param>
    public void SelectSpell(SpellSlot selectedSlot)
    {
        DisplayName(selectedSlot.spell.spellName);
        foreach(SpellSlot slot in spellSlots)
        {
            if (slot == selectedSlot) slot.spellSlot.gameObject.GetComponent<Image>().color = selectedColor;
            else slot.spellSlot.gameObject.GetComponent<Image>().color = unselectedColor;
        }
        selected = selectedSlot;

        // Show Icons
        {
            if (selected.spell.form != null) componentSlots[0].sprite = selected.spell.form.Icon;
            else componentSlots[0].sprite = null;

                for (int i = 0; i < selected.spell.components.Length; i++)
                {
                    if (selected.spell.components[i] != null) componentSlots[i + 1].sprite = selected.spell.components[i].Icon;
                    else componentSlots[i + 1].sprite = null;
                }
        }

        FindComponentSlot();

        DisableDisallowedComponents();
    }
    /// <summary>
    /// Adds a SpellComponent to the spell that is currently being edited, does nothing if all slots are filled up
    /// </summary>
    /// <param name="component"></param>
    public void AddComponent(SpellComponent component)
    {
        if (selectedComponentIndex >= componentSlots.Length) return;

        if (selectedComponentIndex == 0) selected.spell.form = (SpellForm)component;

        else selected.spell.components[selectedComponentIndex - 1] = component;

        componentSlots[selectedComponentIndex].sprite = component.Icon;

        selectedComponentIndex++;

        DisableDisallowedComponents();
    }
    /// <summary>
    /// Removes a SpellComponent at the given index
    /// </summary>
    /// <param name="index"></param>
    public void RemoveComponent(int index)
    {
        if (index == 0) selected.spell.form = null;

        else selected.spell.components[index - 1] = null;

        selectedComponentIndex = index;

        componentSlots[index].sprite = null;

        DisableDisallowedComponents();
    }
    /// <summary>
    /// Displays the name of the spell in the name input section
    /// </summary>
    /// <param name="spellName"></param>
    public void DisplayName(string spellName)
    {
        nameInput.text = spellName;
    }
    /// <summary>
    /// Sets the name of the spell
    /// </summary>
    /// <param name="name"></param>
    public void SetSpellName(string name)
    {
        selected.spell.SetName(name);
    }
    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.None;
        SelectSpell(spellSlots[0]);
    }
    /// <summary>
    /// Finds and selects the first component slot that is not filled in
    /// </summary>
    private void FindComponentSlot()
    {
        if (selected.spell.form == null)
        {
            selectedComponentIndex = 0;
            return;
        }

        for (int i = 0; i < selected.spell.components.Length; i++)
        {
            if (selected.spell.components[i] == null)
            {
                selectedComponentIndex = i + 1;
                return;
            }
        }

        selectedComponentIndex = componentSlots.Length;
    }
    private void DisableDisallowedComponents()
    {
        if (selectedComponentIndex == 0)
        {
            foreach (SpellComponentButton component in components)
            {
                if (component.Component is SpellForm form) component.gameObject.SetActive(true);
                else component.gameObject.SetActive(false);
            }
        }

        else
        {
            SpellComponent lastComponent = null;
            lastComponent = selected.spell.form;

            for (int i = selectedComponentIndex - 2; i >= 0; i--)
            {
                if (selected.spell.components[i] == null) break;

                if (selected.spell.components[i] is SpellEffect effect) lastComponent = effect;
            }

            Debug.Log(lastComponent.name);

            foreach (SpellComponentButton component in components)
            {
                if (lastComponent is SpellForm form)
                {
                    if (form.allowedFollowUps.Contains(component.Component)) component.gameObject.SetActive(true);
                    else component.gameObject.SetActive(false);
                }

                else if (lastComponent is SpellEffect effect)
                {
                    if (effect.allowedFollowUps.Contains(component.Component)) component.gameObject.SetActive(true);
                    else component.gameObject.SetActive(false);
                }
            }
        }
    }
}
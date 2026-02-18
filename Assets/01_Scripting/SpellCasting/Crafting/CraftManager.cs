using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
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
    [SerializeField] private TMP_Text componentNameLabel;
    [SerializeField] private TMP_Text componentDescription;
    [Serializable] private class PlaceHolderSpell
    {
        public SpellForm form;
        public SpellComponent[] components = new SpellComponent[9];
    }
    [SerializeField] private PlaceHolderSpell placeHolderSpell;
    private int selectedComponentIndex;
    public void DisplayComponentInfo(SpellComponent component)
    {
        componentNameLabel.text = component.ComponentName;
        componentDescription.text = component.Description;
    }
    /// <summary>
    /// Attempt to craft the spell, if it's invalid, clear
    /// </summary>
    public void Craft()
    {
        bool isValid = true;

        if (placeHolderSpell.form == null) isValid = false;

        else
        {
            SpellComponent lastComponent = placeHolderSpell.form;

            foreach (SpellComponent component in placeHolderSpell.components)
            {
                if (component == null) continue;

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

        if (!isValid) ClearPlaceholderSpell();

        else
        {
            selected.spell.form = placeHolderSpell.form;
            for (int i = 0; i < placeHolderSpell.components.Length; i++)
            {
                SpellComponent component = placeHolderSpell.components[i];
                selected.spell.components[i] = component == null ? null : component;
            }
        }
    }
    /// <summary>
    /// Clears all component slots of the currently selected spell
    /// </summary>
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
        selectedComponentIndex = 0;
        ClearPlaceholderSpell();
    }
    /// <summary>
    /// Clears only the placeholder spell
    /// </summary>
    private void ClearPlaceholderSpell()
    {
        placeHolderSpell.form = null;
        foreach (Image image in componentSlots)
        {
            image.sprite = null;
        }

        for (int i = 0; i < placeHolderSpell.components.Length; i++)
        {
            placeHolderSpell.components[i] = null;
        }

        selectedComponentIndex = 0;

        DisableDisallowedComponents();
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
            if (selected.spell.form != null)
            {
                componentSlots[0].sprite = selected.spell.form.Icon;
                placeHolderSpell.form = selected.spell.form;
            }
            else
            {
                componentSlots[0].sprite = null;
                placeHolderSpell.form = null;
            }

            for (int i = 0; i < selected.spell.components.Length; i++)
            {
                if (selected.spell.components[i] != null)
                {
                    componentSlots[i + 1].sprite = selected.spell.components[i].Icon;
                    placeHolderSpell.components[i] = selected.spell.components[i];
                }
                else
                {
                    componentSlots[i + 1].sprite = null;
                    placeHolderSpell.components[i] = null;
                }
            }
        }

        FindComponentSlot();

        DisableDisallowedComponents();

        componentNameLabel.text = string.Empty;
        componentDescription.text = string.Empty;
    }
    /// <summary>
    /// Adds a SpellComponent to the spell that is currently being edited, does nothing if all slots are filled up
    /// </summary>
    /// <param name="component"></param>
    public void AddComponent(SpellComponent component)
    {
        if (selectedComponentIndex >= componentSlots.Length) return;

        if (selectedComponentIndex == 0) placeHolderSpell.form = (SpellForm)component;

        else placeHolderSpell.components[selectedComponentIndex - 1] = component;

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
        if (index == 0)
        {
            placeHolderSpell.form = null;
        }

        else
        {
            if (placeHolderSpell.components[index - 1] == null)
            {
                FindComponentSlot();
                DisableDisallowedComponents();
                return;
            }

            placeHolderSpell.components[index - 1] = null;
        }

        selectedComponentIndex = index;

        componentSlots[index].sprite = null;

        FindComponentSlot();

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
        Cursor.lockState = CursorLockMode.None;
    }
    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.None;
        SelectSpell(spellSlots[0]);
    }
    private void OnDisable()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    /// <summary>
    /// Finds and selects the first component slot that is not filled in
    /// </summary>
    private void FindComponentSlot()
    {
        if (placeHolderSpell.form == null)
        {
            selectedComponentIndex = 0;
            return;
        }

        for (int i = 0; i < placeHolderSpell.components.Length; i++)
        {
            if (placeHolderSpell.components[i] == null)
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
            lastComponent = placeHolderSpell.form;

            for (int i = selectedComponentIndex - 2; i >= 0; i--)
            {
                if (placeHolderSpell.components[i] == null) break;

                if (placeHolderSpell.components[i] is SpellEffect effect)
                {
                    lastComponent = effect;
                    break;
                }
            }

            if (lastComponent == null) return;

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
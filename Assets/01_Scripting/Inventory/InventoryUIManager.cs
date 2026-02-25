using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;
/// <summary>
/// Handles equipping and unequipping items in the inventory UI menu
/// </summary>
public class InventoryUIManager : MonoBehaviour
{
    [SerializeField] private Image headButton;
    [SerializeField] private Image chestButton;
    [SerializeField] private Image legButton;
    [SerializeField] private Image footButton;
    [SerializeField] private Image weaponButton;

    [SerializeField] private Inventory inventory;
    [SerializeField] private PlayerStats playerStats;
    #region Equip
    public void EquipItem(Item item)
    {
        Debug.Log("equipping");
        if (item == null) return;

        switch (item)
        {
            case Armor armor:
                EquipArmor(armor);
                break;

            case Weapon weapon:
                inventory.equippedItems.weapon = weapon;
                weaponButton.sprite = weapon.Icon;
                break;

            default:
                Debug.LogWarning($"Item type {item.GetType()} cannot be equipped.");
                return;
        }

        UpdatePlayerStats();
    }

    private void EquipArmor(Armor armor)
    {
        switch (armor.ArmorType)
        {
            case ArmorTypes.Head:
                inventory.equippedItems.head = armor;
                headButton.sprite = armor.Icon;
                break;

            case ArmorTypes.Chest:
                inventory.equippedItems.chest = armor;
                chestButton.sprite = armor.Icon;
                break;

            case ArmorTypes.Leg:
                inventory.equippedItems.legs = armor;
                legButton.sprite = armor.Icon;
                break;

            case ArmorTypes.Foot:
                inventory.equippedItems.feet = armor;
                footButton.sprite = armor.Icon;
                break;

            default:
                Debug.LogWarning($"Unhandled armor type: {armor.ArmorType}");
                break;
        }
    }
    #endregion

    #region Unequip
    public void UnEquipHead()
    {
        inventory.equippedItems.head = null;
        headButton.sprite = null;
        UpdatePlayerStats();
    }
    public void UnEquipChest()
    {
        inventory.equippedItems.chest = null;
        chestButton.sprite = null;
        UpdatePlayerStats();
    }
    public void UnEquipLeg()
    {
        inventory.equippedItems.legs = null;
        legButton.sprite = null;
        UpdatePlayerStats();
    }
    public void UnEquipFoot()
    {
        inventory.equippedItems.feet = null;
        footButton.sprite = null;
        UpdatePlayerStats();
    }
    public void UnEquipWeapon()
    {
        inventory.equippedItems.weapon = null;
        weaponButton.sprite = null;
        UpdatePlayerStats();
    }
    #endregion
    /// <summary>
    /// Updates the players health, mana, etc. upon equipping or unequipping items
    /// </summary>
    private void UpdatePlayerStats()
    {
        List<StatBonus> bonusses = new List<StatBonus>();
        if (inventory.equippedItems.head != null)
            foreach(StatBonus bonus in inventory.equippedItems.head.StatBonusses) bonusses.Add(bonus);

        if (inventory.equippedItems.chest != null)
            foreach (StatBonus bonus in inventory.equippedItems.chest.StatBonusses) bonusses.Add(bonus);

        if (inventory.equippedItems.legs != null)
            foreach (StatBonus bonus in inventory.equippedItems.legs.StatBonusses) bonusses.Add(bonus);

        if (inventory.equippedItems.feet != null)
            foreach (StatBonus bonus in inventory.equippedItems.feet.StatBonusses) bonusses.Add(bonus);

        if (inventory.equippedItems.weapon != null)
            foreach (StatBonus bonus in inventory.equippedItems.weapon.StatBonusses) bonusses.Add(bonus);

        playerStats.UpdateStats(bonusses);
    }
    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.None;
    }
    private void OnDisable()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
}
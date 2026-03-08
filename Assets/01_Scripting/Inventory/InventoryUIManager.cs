using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;
using System;
using TMPro;
/// <summary>
/// Handles equipping and unequipping items in the inventory UI menu
/// </summary>
public class InventoryUIManager : MonoBehaviour
{
    [Header("Equipped Items")]
    [SerializeField] private Image headButton;
    [SerializeField] private Image chestButton;
    [SerializeField] private Image legButton;
    [SerializeField] private Image footButton;
    [SerializeField] private Image weaponButton;

    [Header("References")]
    [SerializeField] private Inventory inventory;
    [SerializeField] private PlayerStats playerStats;

    [Header("Item Info Display")]
    [SerializeField] private Transform itemStatsObject;
    [SerializeField] private TMP_Text itemNameLabel;
    [SerializeField] private Transform statParent;
    [SerializeField] private TMP_Text statPrefab;

    public static Action<Item> ItemEvent;
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
                EquipWeaponEventData data = new EquipWeaponEventData(weapon);
                GameManager.Instance.EquipWeapon(data);
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
            case ArmorTypes.Helmet:
                inventory.equippedItems.head = armor;
                headButton.sprite = armor.Icon;
                break;

            case ArmorTypes.Chestplate:
                inventory.equippedItems.chest = armor;
                chestButton.sprite = armor.Icon;
                break;

            case ArmorTypes.Leggings:
                inventory.equippedItems.legs = armor;
                legButton.sprite = armor.Icon;
                break;

            case ArmorTypes.Footwear:
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
        EquipWeaponEventData data = new EquipWeaponEventData(null);
        GameManager.Instance.EquipWeapon(data);
        UpdatePlayerStats();
    }
    #endregion

    private void UpdateItemInfoDisplay(Item item)
    {
        if (!itemStatsObject.gameObject.activeSelf) itemStatsObject.gameObject.SetActive(true);

        // Clear bonus stats
        for (int i = statParent.childCount - 1; i >= 0; i--)
        {
            Destroy(statParent.GetChild(i).gameObject);
        }

        // Set name
        itemNameLabel.text = item.Name;

        TMP_Text type = Instantiate(statPrefab, Vector3.zero, Quaternion.identity, statParent);
        type.text = "Type: ";
        type.text += item is Armor armor ? armor.ArmorType : "Weapon";

        if (item is Weapon weapon)
        {
            TMP_Text damage = Instantiate(statPrefab, Vector3.zero, Quaternion.identity, statParent);
            damage.text = $"Damage: {weapon.Damage}";

            TMP_Text attackSpeed = Instantiate(statPrefab, Vector3.zero, Quaternion.identity, statParent);
            attackSpeed.text = $"Attack Speed: {weapon.AttackSpeed}";
        }

        foreach (StatBonus bonus in item.StatBonusses)
        {
            TMP_Text bonusStat = Instantiate(statPrefab, Vector3.zero, Quaternion.identity, statParent);
            bonusStat.text = $"{bonus.stat}: +{bonus.bonus}";
        }
    }

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

        playerStats.CalculateFinalStats(bonusses);
    }
    private void OnEnable()
    {
        ItemEvent += UpdateItemInfoDisplay;

        Cursor.lockState = CursorLockMode.None;

        if (inventory.equippedItems.head != null) headButton.sprite = inventory.equippedItems.head.Icon;
        if (inventory.equippedItems.chest != null) chestButton.sprite = inventory.equippedItems.chest.Icon;
        if (inventory.equippedItems.legs != null) legButton.sprite = inventory.equippedItems.legs.Icon;
        if (inventory.equippedItems.feet != null) footButton.sprite = inventory.equippedItems.feet.Icon;
        if (inventory.equippedItems.weapon != null) weaponButton.sprite = inventory.equippedItems.weapon.Icon;
    }
    private void OnDisable()
    {
        ItemEvent -= UpdateItemInfoDisplay;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
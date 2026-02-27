using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Collections;
public class InventoryManager : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private ItemDataBase dataBase;
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private Potion potion;
    private void OnEnable()
    {
        EventBusManager.Instance.GetItemEvent.Register(AddItem);
        EventBusManager.Instance.GameOverEvent.Register(SaveInventoryData);
    }
    private void OnDisable()
    {
        EventBusManager.Instance.GetItemEvent.Unregister(AddItem);
        EventBusManager.Instance.GameOverEvent.Unregister(SaveInventoryData);
    }
    private void AddItem(GetItemEventData data)
    {
        if (data.Item is Potion potion) inventory.AddPotion(potion);
        else inventory.AddItem(data.Item);
    }
    private IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();
        Locator.instance.PotionPresenter.SetValue(0, float.PositiveInfinity, inventory.equippedItems.hpPotions.Count);
        LoadInventoryData();
    }
    private void LoadInventoryData()
    {
        string path = Application.persistentDataPath + "/Inventory.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);  // Read file contents
            InventorySaveData save = JsonUtility.FromJson<InventorySaveData>(json);  // Deserialize

            // Load Armors
            if (save.headID != string.Empty) inventory.equippedItems.head = dataBase.Armors[save.headID];
            if (save.chestID != string.Empty) inventory.equippedItems.chest = dataBase.Armors[save.chestID];
            if (save.legID != string.Empty) inventory.equippedItems.legs = dataBase.Armors[save.legID];
            if (save.footID != string.Empty) inventory.equippedItems.feet = dataBase.Armors[save.footID];

            // Load weapon
            if (save.weaponID != string.Empty) inventory.equippedItems.weapon = dataBase.Weapons[save.weaponID];

            // Load hpPotions
            inventory.equippedItems.hpPotions = new();
            for (int i = 0; i < save.potionAmount; i++) { inventory.AddPotion(potion); }

            // Load Items in Inventory
            inventory.ItemsInInventory.Clear();
            foreach (string id in save.items)
            {
                if (dataBase.Armors.ContainsKey(id)) inventory.ItemsInInventory.Add(dataBase.Armors[id]);
                else if (dataBase.Weapons.ContainsKey(id)) inventory.ItemsInInventory.Add(dataBase.Weapons[id]);
            }
        }

        UpdatePlayerStats();
        if (inventory.equippedItems.weapon != null)
        {
            EquipWeaponEventData data = new EquipWeaponEventData(inventory.equippedItems.weapon);
            EventBusManager.Instance.EquipWeaponEvent.Raise(data);
        }
    }
    private void OnApplicationQuit()
    {
        SaveInventoryData(new GameOverEventData(false));
    }
    private void SaveInventoryData(GameOverEventData data)  
    {
        List<string> items = new List<string>();
        foreach (Item item in inventory.ItemsInInventory)
        {
            items.Add(item.ItemID);
        }

        InventorySaveData savedata = new InventorySaveData
        {
            headID = inventory.equippedItems.head == null ? string.Empty : inventory.equippedItems.head.ItemID,
            chestID = inventory.equippedItems.chest == null ? string.Empty : inventory.equippedItems.chest.ItemID,
            legID = inventory.equippedItems.legs == null ? string.Empty : inventory.equippedItems.legs.ItemID,
            footID = inventory.equippedItems.feet == null ? string.Empty : inventory.equippedItems.feet.ItemID,
            weaponID = inventory.equippedItems.weapon == null ? string.Empty : inventory.equippedItems.weapon.ItemID,
            potionAmount = inventory.equippedItems.hpPotions.Count,
            items = items
        };

        string path = Application.persistentDataPath + "/Inventory.json";
        string json = JsonUtility.ToJson(savedata);
        File.WriteAllText(path, json);
    }
    private void UpdatePlayerStats()
    {
        List<StatBonus> bonusses = new List<StatBonus>();
        if (inventory.equippedItems.head != null)
            foreach (StatBonus bonus in inventory.equippedItems.head.StatBonusses) bonusses.Add(bonus);

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
}
using UnityEngine;
using System;
using System.Collections.Generic;
[Serializable] public class EquippedItems
{
    public Armor head;
    public Armor chest;
    public Armor legs;
    public Armor feet;
    public Weapon weapon;
    public List<Potion> hpPotions;
}
[CreateAssetMenu(fileName = "Inventory", menuName = "Scriptable Objects/Inventory")]
public class Inventory : ScriptableObject
{
    [SerializeField] public EquippedItems equippedItems = new EquippedItems();
    [SerializeField] private List<Item> itemsInInventory = new List<Item>();
    public List<Item> ItemsInInventory => itemsInInventory;
    public void UsePotion()
    {
        if (equippedItems.hpPotions.Count > 0)
        {
            Potion potion = equippedItems.hpPotions[0];
            HealPlayerEventData data = new HealPlayerEventData(potion.Healing, potion.IsOverTime, potion.Time, potion.HasInitialBurst, potion.InitialBurst);
            EventBusManager.instance.HealPlayerEvent.Raise(data);
            equippedItems.hpPotions.Remove(potion);
        }
    }
}
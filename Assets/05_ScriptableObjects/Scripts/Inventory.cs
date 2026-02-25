using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;
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
    public void UsePotion(CallbackContext context)
    {
        if (context.phase != InputActionPhase.Performed) return;

        if (equippedItems.hpPotions.Count > 0)
        {
            Potion potion = equippedItems.hpPotions[equippedItems.hpPotions.Count - 1];
            HealPlayerEventData data = new HealPlayerEventData(potion.Healing, potion.IsOverTime, potion.Time, potion.HasInitialBurst, potion.InitialBurst);
            EventBusManager.Instance.HealPlayerEvent.Raise(data);
            equippedItems.hpPotions.Remove(potion);

            Locator.instance.PotionPresenter.SetValue(0, float.PositiveInfinity, equippedItems.hpPotions.Count);
        }
    }
    public void AddItem(Item item)
    {
        itemsInInventory.Add(item);
    }
    public void AddPotion(Potion potion)
    {
        equippedItems.hpPotions.Add(potion);

        Locator.instance.PotionPresenter.SetValue(0, float.PositiveInfinity, equippedItems.hpPotions.Count);
    }
}
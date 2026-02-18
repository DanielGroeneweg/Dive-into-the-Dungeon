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
    public List<Item> hpPotions;
}
[CreateAssetMenu(fileName = "Inventory", menuName = "Scriptable Objects/Inventory")]
public class Inventory : ScriptableObject
{
    [SerializeField] private EquippedItems equippedItems = new EquippedItems();
    [SerializeField] private List<Item> itemsInInventory = new List<Item>();
    public EquippedItems EquippedItems => equippedItems;
    public List<Item> ItemsInInventory => itemsInInventory;
}
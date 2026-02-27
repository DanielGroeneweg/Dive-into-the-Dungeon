using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ItemDataBase", menuName = "Scriptable Objects/ItemDataBase")]
public class ItemDataBase : ScriptableObject
{
    [SerializeField] private List<Armor> armors = new();
    [SerializeField] private List<Weapon> weapons = new();
    private Dictionary<string, Armor> armorDic = new();
    private Dictionary<string, Weapon> weaponDic = new();
    public Dictionary<string, Armor> Armors => armorDic;
    public Dictionary<string, Weapon> Weapons => weaponDic;
#if UNITY_EDITOR
    private void OnValidate()
    {
        foreach (Armor armor in armors)
        {
            if (!armorDic.ContainsKey(armor.ItemID)) armorDic.Add(armor.ItemID, armor);
        }

        foreach (Weapon weapon in weapons)
        {
            if (!weaponDic.ContainsKey(weapon.ItemID)) weaponDic.Add(weapon.ItemID, weapon);
        }
    }
#endif
}
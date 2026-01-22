using UnityEngine;

[CreateAssetMenu(fileName = "Armor", menuName = "Scriptable Objects/Item/Armor")]
public class Armor : Item
{
    [SerializeField] private ArmorTypes armorType;
    public ArmorTypes ArmorTpe => armorType;
}

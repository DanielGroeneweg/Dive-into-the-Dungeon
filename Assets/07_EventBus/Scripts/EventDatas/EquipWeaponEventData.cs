using UnityEngine;
public class EquipWeaponEventData : GameEventData
{
    public Weapon weapon {  get; private set; }
    public EquipWeaponEventData(Weapon weapon) { this.weapon = weapon; }
}
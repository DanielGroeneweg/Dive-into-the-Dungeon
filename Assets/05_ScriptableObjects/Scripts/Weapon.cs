using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Scriptable Objects/Item/Weapon")]
public class Weapon : Item
{
    [SerializeField] private float damage;
    [SerializeField] private float attackSpeed;
    [SerializeField] private PlayerWeapon prefab;
    public float Damage => damage;
    public float AttackSpeed => attackSpeed;
    public PlayerWeapon Prefab => prefab;
}

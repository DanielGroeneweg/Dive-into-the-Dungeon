using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Scriptable Objects/Item/Weapon")]
public class Weapon : Item
{
    [SerializeField] private float damage;
    [SerializeField] private float attackSpeed;
    public float Damage => damage;
    public float AttackSpeed => attackSpeed;
}

using UnityEngine;
[CreateAssetMenu(fileName = "PlayerStats", menuName = "Scriptable Objects/PlayerStats")]
public class PlayerStats : ScriptableObject
{
    [SerializeField] private float hp;
    [SerializeField] private float maxhp;
    [SerializeField] private float mana;
    [SerializeField] private float maxMana;
    [SerializeField] private float xp;
    public float HP => hp;
    public float MaxHP => maxhp;
    public float Mana => mana;
    public float MaxMana => maxMana;
    public float XP => xp;
    public void TakeDamage(DamagePlayerEventData data)
    {
        hp -= Mathf.Abs(data.damage);
    }
    public void Heal()
    {

    }
}
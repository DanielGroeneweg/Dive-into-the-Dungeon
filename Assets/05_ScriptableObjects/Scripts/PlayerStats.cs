using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "Scriptable Objects/PlayerStats")]
public class PlayerStats : ScriptableObject
{
    public float hp;
    public float maxhp;
    public float mana;
    public float maxMana;
    public float xp;
}
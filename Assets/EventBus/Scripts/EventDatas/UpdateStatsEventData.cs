using UnityEngine;
public class UpdateStatsEventData : GameEventData
{
    public int level {  get; private set; }
    public float currentHealth {  get; private set; }
    public float maxHealth {  get; private set; }
    public float currentMana { get; private set; }
    public float maxMana { get; private set; }
    public float currentXP { get; private set; }
    public float maxXP { get; private set; }
    public UpdateStatsEventData(int level, float currentHealth, float maxHealth, float currentMana, float maxMana, float currentXP, float maxXP)
    {
        this.level = level;
        this.currentHealth = currentHealth;
        this.maxHealth = maxHealth;
        this.currentMana = currentMana;
        this.maxMana = maxMana;
        this.currentXP = currentXP;
        this.maxXP = maxXP;
    }
}
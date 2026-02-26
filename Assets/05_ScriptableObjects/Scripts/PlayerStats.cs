using System;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;
public class PlayerStats : MonoBehaviour
{
    [Header("Player Stats")]
    [SerializeField] private int level;
    [SerializeField] private int maxLevel;
    [SerializeField] private float baseHP;
    [SerializeField] private float hp;
    [SerializeField] private float maxhp;
    [SerializeField] private float hpRegen;
    [SerializeField] private float baseMana;
    [SerializeField] private float mana;
    [SerializeField] private float maxMana;
    [SerializeField] private float manaRegen;
    [SerializeField] private float xp;
    [SerializeField] private float regenTime;

    [Header("Level Up Stats")]
    [SerializeField] private float xpPerLevel;
    [SerializeField] private float hpIncrease;
    [SerializeField] private float hpRegenIncrease;
    [SerializeField] private float manaIncrease;
    [SerializeField] private float manaRegenIncrease;
    #region EventBusSetUp
    private void OnEnable()
    {
        
        EventBusManager.Instance.HealPlayerEvent.Register(HealPlayer);
        EventBusManager.Instance.DamagePlayerEvent.Register(DamagePlayer);
        EventBusManager.Instance.EnemyDeathEvent.Register(GainXP);
        EventBusManager.Instance.LoseManaEvent.Register(RemoveMana);
        EventBusManager.Instance.GainManaEvent.Register(GainMana);
    }
    private void OnDisable()
    {
        EventBusManager.Instance.HealPlayerEvent.Unregister(HealPlayer);
        EventBusManager.Instance.DamagePlayerEvent.Unregister(DamagePlayer);
        EventBusManager.Instance.EnemyDeathEvent.Unregister(GainXP);
        EventBusManager.Instance.LoseManaEvent.Unregister(RemoveMana);
        EventBusManager.Instance.GainManaEvent.Unregister(GainMana);
    }
    #endregion
    private void Start()
    {
        UpdateStatsEventData statsData = new UpdateStatsEventData(level, hp, maxhp, mana, maxMana, xp, xpPerLevel);
        EventBusManager.Instance.UpdateStatsEvent.Raise(statsData);

        StartCoroutine(PassiveRegain());
    }
    /// <summary>
    /// heals the player and grants the player mana over time
    /// </summary>
    /// <returns></returns>
    private IEnumerator PassiveRegain()
    {
        while (true)
        {
            Heal(hpRegen * regenTime);
            GainMana(new GainManaEventData(manaRegen * regenTime));
            yield return new WaitForSeconds(regenTime);
        }
    }
    #region Health
    private void DamagePlayer(DamagePlayerEventData data)
    {
        hp = Mathf.Clamp(hp - Mathf.Abs(data.damage), 0, maxhp);
        UpdateStatsEventData statsData = new UpdateStatsEventData(level, hp, maxhp, mana, maxMana, xp, xpPerLevel);
        EventBusManager.Instance.UpdateStatsEvent.Raise(statsData);
    }
    /// <summary>
    /// Heals the player
    /// </summary>
    /// <param name="health"></param>
    private void Heal(float health)
    {
        hp = Mathf.Clamp(hp + Mathf.Abs(health), 0, maxhp);
        UpdateStatsEventData statsData = new UpdateStatsEventData(level, hp, maxhp, mana, maxMana, xp, xpPerLevel);
        EventBusManager.Instance.UpdateStatsEvent.Raise(statsData);
    }
    /// <summary>
    /// Calls the Heal method in a way corresponding to the healing type
    /// </summary>
    /// <param name="data"></param>
    private void HealPlayer(HealPlayerEventData data)
    {
        if (data.isOverTime)
        {
            if (data.hasInitialBurst) hp = Mathf.Clamp(hp + data.initialBurst, 0, maxhp);

            StartCoroutine(HealingOverTime(data));
        }
        else Heal(data.healing);
    }
    /// <summary>
    /// Heals the player gradually over time
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    private IEnumerator HealingOverTime(HealPlayerEventData data)
    {
        float healPerSecond = data.healing / data.time;
        float healingDone = 0;
        float timePassed = 0;
        yield return new WaitForSeconds(data.healInterval);

        while (healingDone < data.healing)
        {
            timePassed += data.healInterval;
            if (timePassed >= data.time)
            {
                Heal(data.healing - healingDone);
                healingDone = data.healing;
            }

            else
            {
                Heal(healPerSecond * data.healInterval);
                healingDone += healPerSecond * data.healInterval;
            }

            yield return new WaitForSeconds(data.healInterval);
        }
    }
    #endregion

    #region Mana
    private void GainMana(GainManaEventData data)
    {
        mana = Mathf.Clamp(mana + Mathf.Abs(data.mana), 0, maxMana);
        UpdateStatsEventData statsData = new UpdateStatsEventData(level, hp, maxhp, mana, maxMana, xp, xpPerLevel);
        EventBusManager.Instance.UpdateStatsEvent.Raise(statsData);
    }
    private void RemoveMana(LoseManaEventData data)
    {
        mana = Mathf.Clamp(mana - Mathf.Abs(data.mana), 0, maxMana);
        UpdateStatsEventData statsData = new UpdateStatsEventData(level, hp, maxhp, mana, maxMana, xp, xpPerLevel);
        EventBusManager.Instance.UpdateStatsEvent.Raise(statsData);
    }
    /// <summary>
    /// Returns whether the player has enough mana for something
    /// </summary>
    /// <param name="cost"></param>
    /// <returns></returns>
    public bool HasEnoughMana(float cost)
    {
        Debug.Log($"Player has {mana} mana and needs {cost} so returning {mana >= cost}");
        return mana >= cost;
    }
    #endregion

    #region XP
    private void GainXP(EnemyDeathEventData data)
    {
        xp += MathF.Abs(data.XPToGain);

        while (xp >= xpPerLevel && level < maxLevel) LevelUp();
    }
    private void LevelUp()
    {
        xp -= xpPerLevel;
        maxhp += hpIncrease;
        baseHP += hpIncrease;
        hpRegen += hpRegenIncrease;
        maxMana += manaIncrease;
        baseMana += manaIncrease;
        manaRegen += manaRegenIncrease;
        level++;

        UpdateStatsEventData statsData = new UpdateStatsEventData(level, hp, maxhp, mana, maxMana, xp, xpPerLevel);
        EventBusManager.Instance.UpdateStatsEvent.Raise(statsData);
    }
    #endregion
    /// <summary>
    /// Updates the player's stats by combining base stats with stat bonusses from items
    /// </summary>
    /// <param name="statBonusses"></param>
    public void CalculateFinalStats(List<StatBonus> statBonusses)
    {
        maxhp = baseHP;
        maxMana = baseMana;
        foreach (StatBonus statBonus in statBonusses)
        {
            switch (statBonus.stat)
            {
                case StatTypes.MaxHealth:
                    maxhp += statBonus.bonus;
                    break;
                case StatTypes.MaxMana:
                    maxMana += statBonus.bonus;
                    break;
            }
        }

        UpdateStatsEventData statsData = new UpdateStatsEventData(level, hp, maxhp, mana, maxMana, xp, xpPerLevel);
        EventBusManager.Instance.UpdateStatsEvent.Raise(statsData);
    }
}
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
    [SerializeField] private float baseMana;
    [SerializeField] private float mana;
    [SerializeField] private float maxMana;
    [SerializeField] private float xp;

    [Header("Level Up Stats")]
    [SerializeField] private float xpPerLevel;
    [SerializeField] private float hpIncrease;
    [SerializeField] private float manaIncrease;
    #region EventBusSetUp
    private void OnEnable()
    {
        
        EventBusManager.instance.HealPlayerEvent.Register(HealPlayer);
        EventBusManager.instance.DamagePlayerEvent.Register(DamagePlayer);
        EventBusManager.instance.EnemyDeathEvent.Register(GainXP);
        EventBusManager.instance.LoseManaEvent.Register(RemoveMana);
        EventBusManager.instance.GainManaEvent.Register(GainMana);
    }
    private void OnDisable()
    {
        EventBusManager.instance.HealPlayerEvent.Unregister(HealPlayer);
        EventBusManager.instance.DamagePlayerEvent.Unregister(DamagePlayer);
        EventBusManager.instance.EnemyDeathEvent.Unregister(GainXP);
        EventBusManager.instance.LoseManaEvent.Unregister(RemoveMana);
        EventBusManager.instance.GainManaEvent.Unregister(GainMana);
    }
    #endregion

    #region Health
    private void DamagePlayer(DamagePlayerEventData data)
    {
        hp = Mathf.Clamp(hp - Mathf.Abs(data.damage), 0, maxhp);
    }
    private void Heal(float health)
    {
        hp = Mathf.Clamp(hp + Mathf.Abs(health), 0, maxhp);
    }
    private void HealPlayer(HealPlayerEventData data)
    {
        if (data.isOverTime)
        {
            if (data.hasInitialBurst) hp = Mathf.Clamp(hp + data.initialBurst, 0, maxhp);

            StartCoroutine(HealingOverTime(data));
        }
        else Heal(data.healing);
    }
    private IEnumerator HealingOverTime(HealPlayerEventData data)
    {
        float healPerSecond = data.healing / data.time;
        float healingDone = 0;
        float timePassed = 0;
        yield return null;
        while (healingDone < data.healing)
        {
            timePassed += Time.deltaTime;
            if (timePassed >= data.time)
            {
                Heal(data.healing - healingDone);
                healingDone = data.healing;
            }

            else
            {
                Heal(healPerSecond * Time.deltaTime);
                healingDone += healPerSecond * Time.deltaTime;
            }

            yield return null;
        }
    }
    #endregion

    #region Mana
    private void GainMana(GainManaEventData data) { mana = Mathf.Clamp(mana + Mathf.Abs(data.mana), 0, maxMana); }
    private void RemoveMana(LoseManaEventData data) { mana = Mathf.Clamp(mana - Mathf.Abs(data.mana), 0, maxMana); }
    public bool HasEnoughMana(float cost) { return mana <= cost; }
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
        maxMana += manaIncrease;
        baseMana += manaIncrease;

        UpdateStatsEventData statsData = new UpdateStatsEventData(level, hp, maxhp, mana, maxMana, xp, xpPerLevel);
        EventBusManager.instance.UpdateStatsEvent.Raise(statsData);
    }
    public void UpdateStats(List<StatBonus> statBonusses)
    {
        maxhp = baseHP;
        maxMana = baseMana;
        foreach(StatBonus statBonus in statBonusses)
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
        EventBusManager.instance.UpdateStatsEvent.Raise(statsData);
    }
    #endregion
}
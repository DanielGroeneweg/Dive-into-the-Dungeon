using System;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System.IO;
using NaughtyAttributes;
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
        StartCoroutine(Link());        
    }
    private IEnumerator Link()
    {
        yield return new WaitForEndOfFrame();
        GameManager.Instance.LinkHealPlayerEvent(HealPlayer);
        GameManager.Instance.LinkDamagePlayerEvent(DamagePlayer);
        GameManager.Instance.LinkEnemyDeathEvent(GainXP);
        GameManager.Instance.LinkLoseManaEvent(RemoveMana);
        GameManager.Instance.LinkGainManaEvent(GainMana);
        GameManager.Instance.LinkGameOverEvent(SaveStatsData);
    }
    private void OnDisable()
    {
        GameManager.Instance.UnlinkHealPlayerEvent(HealPlayer);
        GameManager.Instance.UnlinkDamagePlayerEvent(DamagePlayer);
        GameManager.Instance.UnlinkEnemyDeathEvent(GainXP);
        GameManager.Instance.UnlinkLoseManaEvent(RemoveMana);
        GameManager.Instance.UnlinkGainManaEvent(GainMana);
        GameManager.Instance.UnlinkGameOverEvent(SaveStatsData);
    }
    #endregion
    private void Start()
    {
        // Load data
        LoadStatsData();

        // Update HUD
        UpdateStatsEventData statsData = new UpdateStatsEventData(level, hp, maxhp, mana, maxMana, xp, xpPerLevel);
        GameManager.Instance.UpdateStats(statsData);

        // Start passive regain
        StartCoroutine(PassiveRegain());
    }
    private void LoadStatsData()
    {
        string path = Application.persistentDataPath + "/Stats.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            PlayerStatsSaveData data = JsonUtility.FromJson<PlayerStatsSaveData>(json);
            EnemyDeathEventData xpData = new EnemyDeathEventData(EnemyTypes.Zombie, (data.level - 1) * xpPerLevel + data.xp);
            GainXP(xpData);
        }
    }
    private void SaveStatsData(GameOverEventData data)
    {
        PlayerStatsSaveData savedata = new PlayerStatsSaveData
        {
            level = this.level,
            xp = this.xp,
        };

        string path = Application.persistentDataPath + "/Stats.json";
        string json = JsonUtility.ToJson(savedata);
        File.WriteAllText(path, json);
    }
    private void OnApplicationQuit()
    {
        SaveStatsData(new GameOverEventData(true));
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
        GameManager.Instance.UpdateStats(statsData);

        if (hp <= 0) GameManager.Instance.GameOver(new GameOverEventData(true));
    }
    /// <summary>
    /// Heals the player
    /// </summary>
    /// <param name="health"></param>
    private void Heal(float health)
    {
        hp = Mathf.Clamp(hp + Mathf.Abs(health), 0, maxhp);
        UpdateStatsEventData statsData = new UpdateStatsEventData(level, hp, maxhp, mana, maxMana, xp, xpPerLevel);
        GameManager.Instance.UpdateStats(statsData);
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
        GameManager.Instance.UpdateStats(statsData);
    }
    private void RemoveMana(LoseManaEventData data)
    {
        Debug.Log($"losing {data.mana} mana");
        mana = Mathf.Clamp(mana - Mathf.Abs(data.mana), 0, maxMana);
        UpdateStatsEventData statsData = new UpdateStatsEventData(level, hp, maxhp, mana, maxMana, xp, xpPerLevel);
        GameManager.Instance.UpdateStats(statsData);
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
        hp += hpIncrease;
        hpRegen += hpRegenIncrease;
        maxMana += manaIncrease;
        baseMana += manaIncrease;
        mana += manaRegen;
        manaRegen += manaRegenIncrease;
        level++;

        UpdateStatsEventData statsData = new UpdateStatsEventData(level, hp, maxhp, mana, maxMana, xp, xpPerLevel);
        GameManager.Instance.UpdateStats(statsData);
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
        GameManager.Instance.UpdateStats(statsData);
    }
#if UNITY_EDITOR
    [SerializeField] private bool debugging;
    [Button("Cheat Level Up", EButtonEnableMode.Playmode)]
    private void CheatLevel()
    {
        if (!debugging) return;
        EnemyDeathEventData data = new EnemyDeathEventData(EnemyTypes.None, xpPerLevel - xp);
        GameManager.Instance.EnemyDeath(data);
    }
#endif
}
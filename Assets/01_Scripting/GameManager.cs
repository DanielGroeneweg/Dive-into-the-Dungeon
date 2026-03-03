using UnityEngine;
using System;
public class GameManager : MonoBehaviour
{
    [SerializeField] private EventBusManager eventBus;
    public static GameManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }
    #region EventBus

    #region EndGameEvent
    public void LinkGameOverEvent(Action<GameOverEventData> action) { eventBus.GameOverEvent.Register(action); }
    public void UnlinkGameOverEvent(Action<GameOverEventData> action) { eventBus.GameOverEvent.Unregister(action); }
    public void GameOver(GameOverEventData data) { eventBus.GameOverEvent.Raise(data); }
    public void EndGame(bool killedByMonster) { eventBus.GameOverEvent.Raise(new GameOverEventData(killedByMonster)); }
    #endregion

    #region HealPlayerEvent
    public void LinkHealPlayerEvent(Action<HealPlayerEventData> action) { eventBus.HealPlayerEvent.Register(action); }
    public void UnlinkHealPlayerEvent(Action<HealPlayerEventData> action) { eventBus.HealPlayerEvent.Unregister(action); }
    public void HealPlayer(HealPlayerEventData data) { eventBus.HealPlayerEvent.Raise(data); }
    #endregion

    #region DamagePlayerEvent
    public void LinkDamagePlayerEvent(Action<DamagePlayerEventData> action) { eventBus.DamagePlayerEvent.Register(action); }
    public void UnlinkDamagePlayerEvent(Action<DamagePlayerEventData> action) { eventBus.DamagePlayerEvent.Unregister(action); }
    public void DamagePlayer(DamagePlayerEventData data) { eventBus.DamagePlayerEvent.Raise(data); }
    #endregion

    #region EnemyDeathEvent
    public void LinkEnemyDeathEvent(Action<EnemyDeathEventData> action) { eventBus.EnemyDeathEvent.Register(action); }
    public void UnlinkEnemyDeathEvent(Action<EnemyDeathEventData> action) { eventBus.EnemyDeathEvent.Unregister(action); }
    public void EnemyDeath(EnemyDeathEventData data) { eventBus.EnemyDeathEvent.Raise(data); }
    #endregion

    #region GetItemEvent
    public void LinkGetItemEvent(Action<GetItemEventData> action) { eventBus.GetItemEvent.Register(action); }
    public void UnlinkGetItemEvent(Action<GetItemEventData> action) { eventBus.GetItemEvent.Unregister(action); }
    public void GetItem(GetItemEventData data) { eventBus.GetItemEvent.Raise(data); }
    #endregion

    #region UpdateStatsEvent
    public void LinkUpdateStatsEvent(Action<UpdateStatsEventData> action) { eventBus.UpdateStatsEvent.Register(action); }
    public void UnlinkUpdateStatsEvent(Action<UpdateStatsEventData> action) { eventBus.UpdateStatsEvent.Unregister(action); }
    public void UpdateStats(UpdateStatsEventData data) { eventBus.UpdateStatsEvent.Raise(data); }
    #endregion

    #region EquipWeaponEvent
    public void LinkEquipWeaponEvent(Action<EquipWeaponEventData> action) { eventBus.EquipWeaponEvent.Register(action); }
    public void UnlinkEquipWeaponEvent(Action<EquipWeaponEventData> action) { eventBus.EquipWeaponEvent.Unregister(action); }
    public void EquipWeapon(EquipWeaponEventData data) { eventBus.EquipWeaponEvent.Raise(data); }
    #endregion

    #region GainManaEvent
    public void LinkGainManaEvent(Action<GainManaEventData> action) { eventBus.GainManaEvent.Register(action); }
    public void UnlinkGainManaEvent(Action<GainManaEventData> action) { eventBus.GainManaEvent.Unregister(action); }
    public void GainMana(GainManaEventData data) { eventBus.GainManaEvent.Raise(data); }
    #endregion

    #region LoseManaEvent
    public void LinkLoseManaEvent(Action<LoseManaEventData> action) { eventBus.LoseManaEvent.Register(action); }
    public void UnlinkLoseManaEvent(Action<LoseManaEventData> action) { eventBus.LoseManaEvent.Unregister(action); }
    public void LoseMana(LoseManaEventData data) { eventBus.LoseManaEvent.Raise(data); }
    #endregion

    #endregion
}
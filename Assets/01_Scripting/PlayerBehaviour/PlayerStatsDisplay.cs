using UnityEngine;
using System.Collections.Generic;
public class PlayerStatsDisplay : MonoBehaviour
{
    [SerializeField] private Presenter[] hpPresenters;
    [SerializeField] private Presenter[] manaPresenters;
    [SerializeField] private Presenter[] xpPresenters;
    private void OnEnable()
    {
        EventBusManager.Instance.UpdateStatsEvent.Register(UpdateStatDisplay);
    }
    private void OnDisable()
    {
        EventBusManager.Instance.UpdateStatsEvent.Unregister(UpdateStatDisplay);
    }
    public void UpdateStatDisplay(UpdateStatsEventData data)
    {
        foreach (Presenter presenter in hpPresenters) if (presenter != null) presenter.SetValue(0, data.maxHealth, data.currentHealth);
        foreach (Presenter presenter in manaPresenters) if (presenter != null) presenter.SetValue(0, data.maxMana, data.currentMana);
        foreach (Presenter presenter in xpPresenters) if (presenter != null) presenter.SetValue(0, data.maxXP, data.currentXP);
    }
}
using UnityEngine;
using System.Collections.Generic;
public class PlayerStatsDisplay : MonoBehaviour
{
    [SerializeField] private Presenter[] hpPresenters;
    [SerializeField] private Presenter[] manaPresenters;
    [SerializeField] private Presenter[] xpPresenters;

    public void UpdateHP(float minValue, float maxValue, float currentValue)
    {
        foreach (Presenter presenter in hpPresenters) if (presenter != null) presenter.SetValue(minValue, maxValue, currentValue);
    }
    public void UpdateMana(float minValue, float maxValue, float currentValue)
    {
        foreach (Presenter presenter in manaPresenters) if (presenter != null) presenter.SetValue(minValue, maxValue, currentValue);
    }
    public void UpdateXP(float minValue, float maxValue, float currentValue)
    {
        foreach (Presenter presenter in xpPresenters) if (presenter != null) presenter.SetValue(minValue, maxValue, currentValue);
    }
}
using TMPro;
using UnityEngine;

public class TextPresenter : Presenter
{
    [SerializeField] private TMP_Text text;
    public override void SetValue(float minValue, float maxValue, float currentValue)
    {
        text.text = $"{Mathf.Clamp(currentValue, minValue, maxValue)}/{maxValue}";
    }
}
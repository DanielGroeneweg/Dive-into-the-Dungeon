using UnityEngine;
using UnityEngine.UI;

public class BarPresenter : Presenter
{
    [SerializeField] private bool fillX = true;
    [SerializeField] private bool fillY = false;
    [SerializeField] private float fillMin;
    [SerializeField] private float fillMax;
    [SerializeField] private RectTransform bar;

    public override void SetFillAmount(float minValue, float maxValue, float currentValue)
    {
        Vector2 trans = bar.sizeDelta;

        float clamped = Mathf.Clamp(currentValue, minValue, maxValue);
        float normalized = (clamped - minValue) / (maxValue - minValue);

        if (fillX)
        {
            trans.x = fillMin + (fillMax - fillMin) * normalized;
        }

        if (fillY)
        {
            trans.y = fillMin + (fillMax - fillMin) * normalized;
        }

        bar.sizeDelta = trans;
    }
}
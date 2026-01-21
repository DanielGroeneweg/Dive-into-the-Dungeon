using UnityEngine;

public abstract class Presenter : MonoBehaviour
{
    public abstract void SetFillAmount(float minValue, float maxValue, float currentValue);
}
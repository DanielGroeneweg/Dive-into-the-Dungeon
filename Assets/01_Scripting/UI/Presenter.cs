using UnityEngine;

public abstract class Presenter : MonoBehaviour
{
    public abstract void SetValue(float minValue, float maxValue, float currentValue);
}
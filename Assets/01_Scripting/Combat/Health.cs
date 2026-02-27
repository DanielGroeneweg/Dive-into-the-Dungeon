using System;
using UnityEngine;
using UnityEngine.Events;
public class Health : MonoBehaviour
{
    [SerializeField] private float maxHealth = 50f;
    [SerializeField] private UnityEvent OnDeath;
    private float current;
    public event Action<float, float, float> healthChanged;
    void Awake()
    {
        current = maxHealth;
    }
    public void TakeDamage(float amount)
    {
        current -= amount;
        Debug.Log($"{gameObject.name} HP: {current}");

        healthChanged.Invoke(0, maxHealth, current);

        if (current <= 0)
        {
            current = 0;
            OnDeath?.Invoke();
            Destroy(gameObject);
        }
    }
}
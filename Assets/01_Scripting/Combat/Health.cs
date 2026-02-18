using UnityEngine;
using UnityEngine.Events;
public class Health : MonoBehaviour
{
    [SerializeField] private float maxHealth = 50f;
    [SerializeField] private UnityEvent OnDeath;
    private float current;
    void Awake()
    {
        current = maxHealth;
    }
    public void TakeDamage(float amount)
    {
        current -= amount;
        Debug.Log($"{gameObject.name} HP: {current}");

        if (current <= 0)
        {
            current = 0;
            OnDeath?.Invoke();
        }
    }
}
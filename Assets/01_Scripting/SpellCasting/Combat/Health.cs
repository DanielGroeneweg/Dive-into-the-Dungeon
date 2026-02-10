using UnityEngine;
public class Health : MonoBehaviour
{
    public float maxHealth = 50f;
    float current;
    void Awake()
    {
        current = maxHealth;
    }
    public void TakeDamage(float amount)
    {
        current -= amount;
        Debug.Log($"{gameObject.name} HP: {current}");

        if (current <= 0)
            Destroy(gameObject);
    }
}
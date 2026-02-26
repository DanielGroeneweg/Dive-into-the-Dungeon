using NaughtyAttributes;
using UnityEngine;
[CreateAssetMenu(fileName = "Potion", menuName = "Scriptable Objects/Item/Potion")]
public class Potion : Item
{
    [SerializeField] private float healing;
    [SerializeField] private bool isOverTime;
    [SerializeField] private float time;
    [SerializeField] private float healInterval;
    [SerializeField] private bool hasInitialBurst;
    [SerializeField] private float initialBurst;

    public float Healing => healing;
    public bool IsOverTime => isOverTime;
    public float Time => time;
    public float HealInterval => healInterval;
    public bool HasInitialBurst => hasInitialBurst;
    public float InitialBurst => initialBurst;
}
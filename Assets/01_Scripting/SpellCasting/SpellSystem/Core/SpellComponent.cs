using UnityEngine;
public abstract class SpellComponent : ScriptableObject
{
    [SerializeField] private string componentName;
    [SerializeField] private Sprite icon;
    [SerializeField] private string description;
    public Sprite Icon { get { return icon; } }
    public string Description { get { return description; } }
    public string ComponentName { get { return componentName; } }
}
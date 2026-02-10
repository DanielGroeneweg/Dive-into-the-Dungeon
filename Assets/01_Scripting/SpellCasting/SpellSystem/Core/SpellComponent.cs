using UnityEngine;
public abstract class SpellComponent : ScriptableObject
{
    [SerializeField] private Sprite icon;
    public Sprite Icon { get { return icon; } }
}
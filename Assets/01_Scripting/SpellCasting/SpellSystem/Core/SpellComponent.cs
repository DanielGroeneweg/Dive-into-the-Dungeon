using UnityEngine;
using static UnityEditor.Progress;
public abstract class SpellComponent : ScriptableObject
{
    [SerializeField] private string componentName;
    [SerializeField] private Sprite icon;
    [SerializeField] private string description;
    [SerializeField] private float manaCost;
    private string spellComponentID;
    public Sprite Icon => icon;
    public string Description => description;
    public string ComponentName => componentName;
    public float ManaCost => manaCost;
    public string SpellComponentID => spellComponentID;
#if UNITY_EDITOR
    // Runs in editor when ScriptableObject is created or validated
    private void OnValidate()
    {
        if (string.IsNullOrEmpty(spellComponentID))
        {
            spellComponentID = System.Guid.NewGuid().ToString();
            UnityEditor.EditorUtility.SetDirty(this); // marks asset as changed
        }
    }
#endif
}
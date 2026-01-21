using UnityEngine;
[CreateAssetMenu(fileName = "Spell", menuName = "Scriptable Objects/Spell")]
public class Spell : ScriptableObject
{
    [SerializeField] private string spellName;
    [SerializeField] private Sprite icon;
    [SerializeField] private float manaCost;
    [SerializeField] private float cooldown;
    [SerializeField, HideInInspector] private string itemID;
    public bool CanCast;
    public string ItemID => itemID;
    public Sprite Icon => icon;
    public float ManaCost => manaCost;
    public float Cooldown => cooldown;
    public string Name => spellName;

#if UNITY_EDITOR
    // Runs in editor when ScriptableObject is created or validated
    private void OnValidate()
    {
        if (string.IsNullOrEmpty(itemID))
        {
            itemID = System.Guid.NewGuid().ToString();
            UnityEditor.EditorUtility.SetDirty(this); // marks asset as changed
        }
    }
#endif
}
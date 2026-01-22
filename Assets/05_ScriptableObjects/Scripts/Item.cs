using UnityEngine;
using System.Collections.Generic;
public class Item : ScriptableObject
{
    [SerializeField] private string itemName;
    [SerializeField] private Sprite icon;
    [SerializeField] private GameObject prefab;
    [SerializeField, HideInInspector] private string itemID;
    [SerializeField] private List<StatBonus> statBonusses = new();
    public string ItemID => itemID;
    public Sprite Icon => icon;
    public string Name => itemName;
    public List<StatBonus> StatBonusses => statBonusses;
    public GameObject Prefab => prefab;

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
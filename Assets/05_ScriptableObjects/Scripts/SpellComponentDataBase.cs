using UnityEngine;
using System.Collections.Generic;
[CreateAssetMenu(fileName = "SpellComponentDataBase", menuName = "Scriptable Objects/SpellComponentDataBase")]
public class SpellComponentDataBase : ScriptableObject
{
    [SerializeField] private List<SpellComponent> spellComponents = new();
    private Dictionary<string, SpellComponent> components = new();
    public List<SpellComponent> SpellComponents => spellComponents;
    public Dictionary<string, SpellComponent> Components => components;
#if UNITY_EDITOR
    private void OnValidate()
    {
        components.Clear();

        foreach(SpellComponent component in spellComponents)
        {
            if (component != null! && component.SpellComponentID != null && components.ContainsKey(component.SpellComponentID)) components.Add(component.SpellComponentID, component);
        }
        UnityEditor.EditorUtility.SetDirty(this);
    }
#endif
}
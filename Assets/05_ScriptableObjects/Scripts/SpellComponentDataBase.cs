using UnityEngine;
using System.Collections.Generic;
[CreateAssetMenu(fileName = "SpellComponentDataBase", menuName = "Scriptable Objects/SpellComponentDataBase")]
public class SpellComponentDataBase : ScriptableObject
{
    [SerializeField] private List<SpellComponent> spellComponents = new();
    private Dictionary<string, SpellComponent> components = new();
    public List<SpellComponent> SpellComponents => spellComponents;
    public Dictionary<string, SpellComponent> Components => components;
    private void OnEnable()
    {
        BuildDictionary();
    }

    private void BuildDictionary()
    {
        components.Clear();

        foreach (SpellComponent component in spellComponents)
        {
            if (component == null) continue;
            if (string.IsNullOrEmpty(component.SpellComponentID)) continue;

            if (!components.ContainsKey(component.SpellComponentID))
                components.Add(component.SpellComponentID, component);
        }
    }
}
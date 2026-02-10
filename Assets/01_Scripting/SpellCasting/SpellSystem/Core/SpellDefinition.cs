using System.Collections.Generic;
using UnityEngine;
using static CraftManager;

[CreateAssetMenu(menuName = "Spells/Spell Definition")]
public class SpellDefinition : ScriptableObject
{
    public string spellName;
    public SpellForm form;
    public SpellComponent[] components = new SpellComponent[9];
    public void SetName(string input)
    {
        spellName = input;
    }
}
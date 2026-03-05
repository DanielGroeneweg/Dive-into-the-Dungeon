using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// A class containing information about spells that are cast
/// </summary>
public class SpellContext
{
    public GameObject caster;
    public GameObject target;
    public List<ModifiedEffect> effects;
    public Vector3 spellPosition;
    public Quaternion spellRotation;
}
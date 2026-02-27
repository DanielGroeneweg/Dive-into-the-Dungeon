#if UNITY_EDITOR
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(SpellComponent), true)]
public class SpellComponentEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        SpellComponent component = (SpellComponent)target;
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Spell ID", component.SpellComponentID);
    }
}
[CustomEditor(typeof(Item), true)]
public class ItemEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Item item = (Item)target;
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Item ID", item.ItemID);
    }
}
#endif
#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(Spell), true)]
public class SpellEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Spell item = (Spell)target;
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Item ID", item.ItemID);
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
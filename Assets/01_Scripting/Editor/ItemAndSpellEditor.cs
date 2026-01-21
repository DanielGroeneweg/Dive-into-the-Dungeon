#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(Spell), true)]
public class ItemAndSpellEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Spell item = (Spell)target;
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Item ID", item.ItemID);
    }
}
#endif
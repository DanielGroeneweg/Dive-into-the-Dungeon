#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
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
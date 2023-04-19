using UnityEngine;
using UnityEditor;
using UnityEngine.InputSystem;

[CustomEditor(typeof(ItemsPack))]
public class ItemsPackEditor : Editor
{
    private bool _inBox;
    private ItemsPack _itemsPack;
 
    public override void OnInspectorGUI()
    {
        _itemsPack = (ItemsPack)target;
        DrawDefaultInspector();

        _inBox = _itemsPack.InBox;

        if (_inBox)
        {
            serializedObject.Update();

            var so = new SerializedObject(_itemsPack);
            SerializedProperty x = so.FindProperty("_itemCountX");
            SerializedProperty y = so.FindProperty("_itemCountY");
            SerializedProperty z = so.FindProperty("_itemCountZ");
            
            EditorGUILayout.BeginVertical();

            EditorGUILayout.Space();

            GUI.color = Color.red;
            x.intValue = EditorGUILayout.IntField("Item Count X", x.intValue);
            GUI.color = Color.green;
            y.intValue = EditorGUILayout.IntField("Item Count Y", y.intValue);
            GUI.color = Color.blue;
            z.intValue = EditorGUILayout.IntField("Item Count Z", z.intValue);

            EditorGUILayout.EndVertical();

            so.ApplyModifiedProperties();
        }
    }
}
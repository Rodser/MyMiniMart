using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(ItemsPack))]
    public class ItemsPackEditor : UnityEditor.Editor
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

        [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
        public static void RenderCustomGizmo(ItemsPack pack, GizmoType gizmo)
        {
            Gizmos.color = Color.green;

            for (int i = 0; i < pack.MaxItems; i++)
            {
                Gizmos.DrawSphere(pack.GetPosition(i), 0.15f);
            }
        }
    }
}
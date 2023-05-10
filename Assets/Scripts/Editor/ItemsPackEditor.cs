using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(ItemsPack))]
    public class ItemsPackEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            ItemsPack itemsPack = (ItemsPack)target;
            DrawDefaultInspector();

            var serObject = new SerializedObject(itemsPack);
            
            if (itemsPack.InBox)
            {
                SerializedProperty x = serObject.FindProperty("_itemCountX");
                SerializedProperty y = serObject.FindProperty("_itemCountY");
                SerializedProperty z = serObject.FindProperty("_itemCountZ");

                EditorGUILayout.BeginVertical();

                EditorGUILayout.Space();

                GUI.color = Color.red;
                x.intValue = EditorGUILayout.IntField("Item Count X", x.intValue);
                GUI.color = Color.green;
                y.intValue = EditorGUILayout.IntField("Item Count Y", y.intValue);
                GUI.color = Color.blue;
                z.intValue = EditorGUILayout.IntField("Item Count Z", z.intValue);

                EditorGUILayout.EndVertical();
            }

            serObject.ApplyModifiedProperties();
        }

        [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
        public static void RenderCustomGizmo(ItemsPack pack, GizmoType gizmo)
        {
            Gizmos.color = pack.Marker.Color;

            for (int i = 0; i < pack.MaxItems; i++)
            {
                Gizmos.DrawSphere(pack.GetPosition(i), pack.Marker.SizeGizmo);
            }
        }
    }
}
using Logic;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(Marker))]
    public class MarkerEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            var marker = (Marker)target;
            var serObject = new SerializedObject(marker);
            
            SerializedProperty color = serObject.FindProperty("Color");
            SerializedProperty size = serObject.FindProperty("SizeGizmo");
            
            EditorGUILayout.BeginVertical();
            EditorGUILayout.Space();
     
            color.colorValue = EditorGUILayout.ColorField("Color", color.colorValue);
            size.floatValue = EditorGUILayout.FloatField("Size Gizmo", size.floatValue);
            
            EditorGUILayout.EndVertical();
            serObject.ApplyModifiedProperties();
        }

        [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
        public static void RenderCustomGizmo(Marker marker, GizmoType gizmo)
        {
            Gizmos.color = marker.Color;
            Gizmos.DrawSphere(marker.transform.position, marker.SizeGizmo);
        }
    }
}
using System;
using Logic;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(Spawner))]
    public class SpawnerEditor : UnityEditor.Editor
    {
        private Color _color;
        private SpawnerType _spawner;
        private bool _has;
        private readonly Func<Enum,bool> changeSpawner = null;

        public override void OnInspectorGUI()
        {
           // DrawDefaultInspector();
            
            var spawner = (Spawner)target;
            var so = new SerializedObject(spawner);
            SerializedProperty spawnerType = so.FindProperty("SpawnerType");
            SerializedProperty spawnerColor = so.FindProperty("Color");
            EditorGUILayout.BeginVertical();
            EditorGUILayout.Space();
     
            spawnerColor.colorValue = EditorGUILayout.ColorField("Color Spawner", spawnerColor.colorValue);
            
            if (GUILayout.Button("Set"))
            {
            }

            EditorGUILayout.EndVertical();
            so.ApplyModifiedProperties();
        }

        [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
        public static void RenderCustomGizmo(Spawner spawner, GizmoType gizmo)
        {
            Gizmos.color = spawner.Color;
            Gizmos.DrawSphere(spawner.transform.position, 0.4f);
        }
    }
}
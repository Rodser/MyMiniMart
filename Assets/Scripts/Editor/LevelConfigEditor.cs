using Infrastructure.Services.Configs;
using Logic;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(LevelConfig))]
    public class LevelConfigEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            var levelConfig = (LevelConfig)target;
            var serObject = new SerializedObject(levelConfig);
            
            EditorGUILayout.BeginVertical();
            EditorGUILayout.Space();
     
            if (GUILayout.Button("Records Spawners"))
            {
                Spawner[] heroSpawners = GameObject.FindObjectsByType<Spawner>(FindObjectsSortMode.None);
                levelConfig.Spawners = heroSpawners;
            }

            EditorGUILayout.EndVertical();
            serObject.ApplyModifiedProperties();
        }
    }
}
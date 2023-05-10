using System.Collections.Generic;
using System.Linq;
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
                List<SpawnerData> heroSpawners = FindObjectsByType<Spawner>(FindObjectsSortMode.None)
                    .Select(s => new SpawnerData(s.Marker, s.transform.position))
                    .ToList();
                
                levelConfig.Spawners = heroSpawners;
            }

            EditorGUILayout.EndVertical();
            serObject.ApplyModifiedProperties();
        }
    }
}
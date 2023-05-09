using System.Collections.Generic;
using Hero;
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
            var so = new SerializedObject(levelConfig);
            
            SerializedProperty position = so.FindProperty("HeroPosition");
            
            EditorGUILayout.BeginVertical();
            EditorGUILayout.Space();
     
            if (GUILayout.Button("Find Spawners"))
            {
                var spawners = new List<ISpawner>();
                var heroPoint = GameObject.FindAnyObjectByType<HeroSpawner>();
                spawners.Add(heroPoint);

                levelConfig.HeroSpawner = heroPoint;
                position.vector3Value = EditorGUILayout.Vector3Field("Hero Position", position.vector3Value);
                levelConfig.Spawners = spawners;
            }

            EditorGUILayout.EndVertical();
            so.ApplyModifiedProperties();
        }
    }
}
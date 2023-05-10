using System;
using System.Collections.Generic;
using System.Linq;
using Logic;
using UnityEngine;

namespace Infrastructure.Services.Configs
{
    [CreateAssetMenu(fileName = "LevelConfig", menuName = "Game/LevelConfig")]
    public class LevelConfig : ScriptableObject
    {
        public List<SpawnerData> Spawners;

        public SpawnerData GetSpawner(SpawnerMarker marker)
        {
           return Spawners.FirstOrDefault(spawner => spawner.Marker == marker);
        }
    }

    [Serializable]
    public class SpawnerData
    {
        public SpawnerMarker Marker;
        public Vector3 Position;

        public SpawnerData(SpawnerMarker marker, Vector3 position)
        {
            Marker = marker;
            Position = position;
        }
    }
}
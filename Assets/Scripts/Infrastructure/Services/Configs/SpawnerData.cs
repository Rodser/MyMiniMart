using System;
using Logic;
using UnityEngine;

namespace Infrastructure.Services.Configs
{
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
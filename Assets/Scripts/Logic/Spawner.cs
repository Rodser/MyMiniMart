using System;
using UnityEngine;

namespace Logic
{
    [RequireComponent(typeof(Marker))]
    [Serializable]
    public class Spawner : MonoBehaviour, ISpawner
    {
        public SpawnerMarker Marker;
        public Vector3 Position;
        }
}
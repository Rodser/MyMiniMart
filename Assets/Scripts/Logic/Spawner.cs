using UnityEngine;

namespace Logic
{
    [RequireComponent(typeof(Marker))]
    public class Spawner : MonoBehaviour, ISpawner
    {
        public SpawnerMarker Marker;
        public Vector3 Position;
    }
}
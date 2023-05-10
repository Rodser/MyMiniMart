using System.Linq;
using Logic;
using UnityEngine;

namespace Infrastructure.Services.Configs
{
    [CreateAssetMenu(fileName = "LevelConfig", menuName = "Game/LevelConfig")]
    public class LevelConfig : ScriptableObject
    {
        public Spawner[] Spawners;

        public Spawner GetSpawner(SpawnerMarker marker)
        {
            return Spawners.FirstOrDefault(spawner => spawner.Marker == marker);
        }
    }
}
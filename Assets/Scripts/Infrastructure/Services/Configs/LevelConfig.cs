using System.Collections.Generic;
using Hero;
using Logic;
using UnityEngine;

namespace Infrastructure.Services.Configs
{
    [CreateAssetMenu(fileName = "LevelConfig", menuName = "Game/LevelConfig")]
    public class LevelConfig : ScriptableObject
    {
        public List<ISpawner> Spawners;
        public HeroSpawner HeroSpawner;
        public Vector3 HeroPosition;

        public T GetSpawner<T>() where T : ISpawner, new()
        {
            foreach (var spawner in Spawners)
            {
                if (spawner is T spawn)
                {
                    return spawn;
                }
            }

            return new T();
        }
    }
}
using System;

namespace Data
{
    [Serializable]
    public class PlayerProgress
    {
        public WorldData WorldData;

        public PlayerProgress(string initialSceneName)
        {
            WorldData = new WorldData(initialSceneName);
        }
    }
}
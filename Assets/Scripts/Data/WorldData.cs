using System;

namespace Data
{
    [Serializable]
    public class WorldData
    {
        public PositionOnLevel PositionOnLevel;

        public WorldData(string initialSceneName)
        {
            PositionOnLevel = new PositionOnLevel(initialSceneName);
        }
    }
}
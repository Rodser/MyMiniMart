using UnityEngine;

namespace Infrastructure.Services.Configs
{
    public class ConfigService : IConfigService
    {
        public T GetConfig<T>(string path) where T : ScriptableObject
        {
            return Resources.Load<T>(path);
        }
    }
}
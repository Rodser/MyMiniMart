using UnityEngine;

namespace Infrastructure.Services.Configs
{
    public interface IConfigService : IService
    {
        T GetConfig<T>(string path) where T : ScriptableObject;
    }
}
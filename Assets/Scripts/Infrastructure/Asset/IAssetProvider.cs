using Infrastructure.Services;
using UnityEngine;

namespace Infrastructure.Asset
{
    public interface IAssetProvider : IService
    {
        GameObject Instantiate(string path);
        GameObject Instantiate(string path, Vector3 at);
    }
}
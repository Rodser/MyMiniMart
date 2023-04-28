using Infrastructure.Services;
using UnityEngine;

namespace Infrastructure.Factories
{
    public interface IGameFactory : IService
    {
        GameObject CreateHero(GameObject at);
        void CreateHud();
    }
}
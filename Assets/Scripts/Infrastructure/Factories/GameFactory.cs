using Infrastructure.Asset;
using UnityEngine;

namespace Infrastructure.Factories
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assets;

        public GameFactory(IAssetProvider assets)
        {
            _assets = assets;
        }
        
        public GameObject CreateHero(GameObject at)
        {
            return _assets.Instantiate(AssetPath.PlayerPath, at.transform.position);
        }

        public void CreateHud()
        {
            _assets.Instantiate(AssetPath.HudPath);
        }
    }
}
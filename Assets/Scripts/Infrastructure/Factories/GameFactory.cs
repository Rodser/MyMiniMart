using System.Collections.Generic;
using Hero;
using Infrastructure.Asset;
using Infrastructure.Services;
using Infrastructure.Services.Configs;
using Infrastructure.Services.Input;
using Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace Infrastructure.Factories
{
    public class GameFactory : IGameFactory
    {
        private readonly AllServices _container;
        private readonly IAssetProvider _assets;

        public GameFactory(AllServices container)
        {
            _container = container;
            _assets = container.Single<IAssetProvider>();
        }

        public List<ISaveProgressReader> ProgressReaders { get; set; } = new List<ISaveProgressReader>(); 
        public List<ISaveProgress> ProgressWriters { get; set; } = new List<ISaveProgress>();

        public GameObject CreateHero(GameObject at)
        {
            GameObject hero = InstantiateRegistered(AssetPath.HeroPath, at.transform.position);
            HeroAnimator animator = hero.GetComponent<HeroAnimator>();

            HeroInterplay heroInterplay = hero.GetComponent<HeroInterplay>();
            ItemsPack pack = hero.GetComponentInChildren<ItemsPack>();
            heroInterplay.Construct(pack, animator);

            HeroConfig config = _container.Single<IConfigService>().GetConfig<HeroConfig>(AssetPath.HeroConfigPath);
            HeroMove heroMove = hero.GetComponent<HeroMove>();
            heroMove.Construct(config , animator, _container.Single<IInputService>());

            return hero;
        }

        public void CreateHud() => 
            InstantiateRegistered(AssetPath.HudPath);

        public void CleanUp()
        {
            ProgressWriters.Clear();
            ProgressReaders.Clear();
        }

        private GameObject InstantiateRegistered(string prefabPath, Vector3 at)
        {
            GameObject go = _assets.Instantiate(prefabPath, at);
            RegisterProgressWatcher(go);
            return go;
        }
        
        private GameObject InstantiateRegistered(string prefabPath)
        {
            GameObject go = _assets.Instantiate(prefabPath);
            RegisterProgressWatcher(go);
            return go;
        }

        private void RegisterProgressWatcher(GameObject go)
        {
            foreach (ISaveProgressReader progressReader in go.GetComponentsInChildren<ISaveProgressReader>())
            {
                Register(progressReader);
            }
        }

        private void Register(ISaveProgressReader progressReader)
        {
            if(progressReader is ISaveProgress progress)
                ProgressWriters.Add(progress);
            ProgressReaders.Add(progressReader);
        }
    }
}
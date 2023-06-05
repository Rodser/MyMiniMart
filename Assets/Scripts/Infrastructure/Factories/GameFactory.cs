using System.Collections.Generic;
using Hero;
using Infrastructure.Asset;
using Infrastructure.Services;
using Infrastructure.Services.Configs;
using Infrastructure.Services.Input;
using Infrastructure.Services.PersistentProgress;
using Logic;
using Subject;
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

        public void CreateWorld()
        {
            SpawnerData heroData = GetSpawnerData(SpawnerMarker.Hero);
            GameObject hero = CreateHero(heroData.Position);

            SpawnerData gardenBedData = GetSpawnerData(SpawnerMarker.GardenBed);
            GameObject gardenBed = CreateGardenBed(gardenBedData.Position);

        }

        public void CreateHud() => 
            InstantiateRegistered(AssetPath.HudPath);

        public void CleanUp()
        {
            ProgressWriters.Clear();
            ProgressReaders.Clear();
        }
        
        private GameObject CreateGardenBed(Vector3 at)
        {
            GardenBedConfig bedConfig = _container.Single<IConfigService>().GetConfig<GardenBedConfig>(AssetPath.GardenBedConfigPath);
            GameObject bed = InstantiateRegistered(bedConfig.GardenBed, at);
            GardenBed gardenBed = bed.GetComponent<GardenBed>();
            gardenBed.StartGrows(bedConfig.Plant);
            return bed;
        }

        private SpawnerData GetSpawnerData(SpawnerMarker spawnerMarker)
        {
            var levelConfig = _container.Single<IConfigService>().GetConfig<LevelConfig>(AssetPath.LevelConfigPath);
            return levelConfig.GetSpawner(spawnerMarker);
        }

        private GameObject CreateHero(Vector3 at)
        {
            HeroConfig config = _container.Single<IConfigService>().GetConfig<HeroConfig>(AssetPath.HeroConfigPath);
            // GameObject hero = InstantiateRegistered(AssetPath.HeroPath, at);
            var hero = InstantiateRegistered(config.HeroPrefab);

            HeroAnimator animator = hero.GetComponent<HeroAnimator>();

            HeroInterplay heroInterplay = hero.GetComponent<HeroInterplay>();
            ItemsPack pack = hero.GetComponentInChildren<ItemsPack>();
            heroInterplay.Construct(pack, animator);

            HeroMove heroMove = hero.GetComponent<HeroMove>();
            heroMove.Construct(config , animator, _container.Single<IInputService>());

            GameObject cam = InstantiateRegistered(AssetPath.CameraPath, at);
            CameraFollow cameraFollow = cam.GetComponentInChildren<CameraFollow>();
            cameraFollow.Construct(hero.transform);
            
            return hero;
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
  
        private GameObject InstantiateRegistered(GameObject prefab)
        {
            GameObject go = Object.Instantiate(prefab);
            RegisterProgressWatcher(go);
            return go;
        }
        
        private GameObject InstantiateRegistered(GameObject prefab, Vector3 at)
        {
            GameObject go = Object.Instantiate(prefab, at, Quaternion.identity);
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
using Hero;
using Infrastructure.Asset;
using Infrastructure.Services;
using Infrastructure.Services.Configs;
using Infrastructure.Services.Input;
using Logic;
using Subject;
using UnityEngine;

namespace Infrastructure.Factories
{
    public class GameFactory : Factory, IGameFactory
    {
        public GameFactory(AllServices container) : base(container)
        {
        }

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
            GardenBedConfig bedConfig = Container.Single<IConfigService>().GetConfig<GardenBedConfig>(AssetPath.GardenBedConfigPath);
            GameObject bed = InstantiateRegistered(bedConfig.GardenBed, at);
            GardenBed gardenBed = bed.GetComponent<GardenBed>();
            gardenBed.StartGrows(bedConfig.Plant);
            return bed;
        }

        private SpawnerData GetSpawnerData(SpawnerMarker spawnerMarker)
        {
            var levelConfig = Container.Single<IConfigService>().GetConfig<LevelConfig>(AssetPath.LevelConfigPath);
            return levelConfig.GetSpawner(spawnerMarker);
        }

        private GameObject CreateHero(Vector3 at)
        {
            HeroConfig config = Container.Single<IConfigService>().GetConfig<HeroConfig>(AssetPath.HeroConfigPath);
            // GameObject hero = InstantiateRegistered(AssetPath.HeroPath, at);
            var hero = InstantiateRegistered(config.HeroPrefab);

            HeroAnimator animator = hero.GetComponent<HeroAnimator>();

            HeroInterplay heroInterplay = hero.GetComponent<HeroInterplay>();
            ItemsPack pack = hero.GetComponentInChildren<ItemsPack>();
            heroInterplay.Construct(pack, animator);

            HeroMove heroMove = hero.GetComponent<HeroMove>();
            heroMove.Construct(config , animator, Container.Single<IInputService>());

            GameObject cam = InstantiateRegistered(AssetPath.CameraPath, at);
            CameraFollow cameraFollow = cam.GetComponentInChildren<CameraFollow>();
            cameraFollow.Construct(hero.transform);
            
            return hero;
        }
    }
}
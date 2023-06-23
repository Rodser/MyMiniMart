using Hero;
using Infrastructure.Asset;
using Infrastructure.Services;
using Infrastructure.Services.Configs;
using Infrastructure.Services.Input;
using Logic;
using Subject;
using System;
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

            GameObject gardenBed = CreateGardenBed(GetSpawnerData(SpawnerMarker.GardenBed).Position);
            GameObject shelf = CreateShelf(GetSpawnerData(SpawnerMarker.Shelf).Position);
            GameObject cashDesk = CreateCashDesk(GetSpawnerData(SpawnerMarker.CashDesk).Position);
            GameObject buyer = CreateBuyer(GetSpawnerData(SpawnerMarker.Buyer).Position, shelf, cashDesk);

        }

        public void CreateHud() => 
            InstantiateRegistered(AssetPath.HudPath);

        public void CleanUp()
        {
            ProgressWriters.Clear();
            ProgressReaders.Clear();
        }

        private GameObject CreateBuyer(Vector3 at, GameObject shelf, GameObject cashDesk)
        {
            var config = Container.Single<IConfigService>().GetConfig<BuyerConfig>(AssetPath.BuyerConfigPath);
            var subject = InstantiateRegistered(config.Buyer, at);
            var buyer = subject.GetComponent<Buyer>();
            MoveAnimator animator = buyer.GetComponent<MoveAnimator>();
            buyer.Construct(
                animator,
                shelf.GetComponent<Shelf>().TargetBuyer.position,
                cashDesk.GetComponent<CashDesk>().TargetBuyer.position,
                GetSpawnerData(SpawnerMarker.Away).Position);
            return subject;
        }

        private GameObject CreateCashDesk(Vector3 at)
        {
            var config = Container.Single<IConfigService>().GetConfig<CashDeskConfig>(AssetPath.CashDeskConfigPath);
            var subject = InstantiateRegistered(config.CashDesk, at);
            return subject;
        }

        private GameObject CreateShelf(Vector3 at)
        {
            var config = Container.Single<IConfigService>().GetConfig<ShelfConfig>(AssetPath.ShelfConfigPath);
            var subject = InstantiateRegistered(config.Shelf, at);
            return subject;
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
            var hero = InstantiateRegistered(config.HeroPrefab);

            MoveAnimator animator = hero.GetComponent<MoveAnimator>();

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
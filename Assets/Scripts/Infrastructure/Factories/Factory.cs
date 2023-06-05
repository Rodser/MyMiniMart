using System.Collections.Generic;
using Infrastructure.Asset;
using Infrastructure.Services;
using Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace Infrastructure.Factories
{
    public abstract class Factory
    {
        protected readonly AllServices Container;
        private readonly IAssetProvider _assets;

        protected Factory(AllServices container)
        {
            Container = container;
            _assets = container.Single<IAssetProvider>();
        }
        
        public List<ISaveProgressReader> ProgressReaders { get; set; } = new List<ISaveProgressReader>(); 
        public List<ISaveProgress> ProgressWriters { get; set; } = new List<ISaveProgress>();
        
        protected GameObject InstantiateRegistered(string prefabPath, Vector3 at)
        {
            GameObject go = _assets.Instantiate(prefabPath, at);
            RegisterProgressWatcher(go);
            return go;
        }
        
        protected GameObject InstantiateRegistered(string prefabPath)
        {
            GameObject go = _assets.Instantiate(prefabPath);
            RegisterProgressWatcher(go);
            return go;
        }
  
        protected GameObject InstantiateRegistered(GameObject prefab)
        {
            GameObject go = Object.Instantiate(prefab);
            RegisterProgressWatcher(go);
            return go;
        }
        
        protected GameObject InstantiateRegistered(GameObject prefab, Vector3 at)
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
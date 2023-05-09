using System.Collections.Generic;
using Infrastructure.Services;
using Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace Infrastructure.Factories
{
    public interface IGameFactory : IService
    {
        public List<ISaveProgressReader> ProgressReaders { get; set; }
        public List<ISaveProgress> ProgressWriters { get; set; }
       void CreateHud();
        void CleanUp();
        void CreateWorld();
    }
}
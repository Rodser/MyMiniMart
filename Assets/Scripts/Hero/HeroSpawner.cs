using Logic;
using UnityEngine;

namespace Hero
{
    public class HeroSpawner : MonoBehaviour, ISpawner
    {
        public Vector3 Position => gameObject.transform.position;

        public void Spawn()
        {
            // TODO: нужна фабрика
            // var buyer = Object.Instantiate(_prefab, transform.position, Quaternion.identity);
        }
    }
}
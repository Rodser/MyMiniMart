using System.Threading.Tasks;
using UnityEditor.Rendering;
using UnityEngine;

namespace _Ollie.Scripts
{
    public class Plant : MonoBehaviour
    {
        [SerializeField] private int _growthTime = 1000;
        [SerializeField] private Transform[] _landings;
        [SerializeField] private GameObject _vegetable;
        
        private bool _isFull = false;

        private async void Start()
        {
            while (!_isFull)
            {
               await SpawnVegetables();
            }
        }

        private async Task SpawnVegetables()
        {
            foreach (Transform landing in _landings)
            {
                await Task.Delay(_growthTime);

                Instantiate(_vegetable, landing);
            }

            _isFull = true;
        }
    }
}
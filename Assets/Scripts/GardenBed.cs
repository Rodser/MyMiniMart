using _Ollie.Scripts;
using UnityEngine;

public class GardenBed : MonoBehaviour
{
    [SerializeField] private Transform[] _landings;
    [SerializeField] private Plant _plant;

    private void Start()
    {
        Grow(_landings[0]);
    }

    private void Grow(Transform point)
    {
        Instantiate(_plant, point);
    }
}
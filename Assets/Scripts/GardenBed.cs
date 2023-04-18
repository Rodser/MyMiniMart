using UnityEngine;

public class GardenBed : MonoBehaviour
{
    [SerializeField] private Transform[] _landings;
    [SerializeField] private Plant _plant;

    private void Start()
    {
        for (int i = 0; i < _landings.Length; i++)
        {
            Grow(_landings[i]);
        }
    }

    private void Grow(Transform point)
    {
        Instantiate(_plant, point);
    }
}
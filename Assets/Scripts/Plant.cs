using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class Plant : MonoBehaviour
{
    [SerializeField] private int _growthTime = 1000;
    [SerializeField] private int _timeBusy = 1000;
    [SerializeField] private Transform[] _landings;
    [SerializeField] private Vegetable _vegetable;

    private bool _isFull = false;
    private List<Vegetable> _vegetables = new List<Vegetable>();
    private bool _isBusy;

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
            var go = Instantiate(_vegetable, landing);
            _vegetables.Add(go);
        }

        _isFull = true;
    }

    public Vegetable GetVegetable()
    {
        if (_isBusy)
            return null; 
        
        var veg = _vegetables.FirstOrDefault(vegetable => vegetable != null);
        if (veg != null)
        {
            _vegetables.Remove(veg);
            _isFull = false; 
            Busy();
        }

        return veg;
    }

    private async void Busy()
    {
        _isBusy = true;
        await Task.Delay(_timeBusy);
        _isBusy = false;
    }
}
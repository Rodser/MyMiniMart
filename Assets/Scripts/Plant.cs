using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class Plant : MonoBehaviour
{
    [SerializeField] private int _growthTime = 1000;
    [SerializeField] private int _timeBusy = 1000;
    [SerializeField] private Transform[] _landings;
    [SerializeField] private Vegetable _vegetable;

    private Vegetable[] _vegetables;
    private bool _isBusy;
    private bool _canGrow;

    private void Start()
    {
        _vegetables = new Vegetable[_landings.Length];
        _canGrow = true;
        SpawnVegetables();
    }

    private async void SpawnVegetables()
    {
        if(_canGrow == false)
        {
            return;
        }
        
        _canGrow = false;
        for (var index = 0; index < _landings.Length; index++)
        {
            if(_vegetables[index] != null)
                continue;
                
            await Task.Delay(_growthTime);
            var vegetable = Instantiate(_vegetable, _landings[index]);
            vegetable.CurrentIndex = index;
            _vegetables[index] = vegetable;
        }

        _canGrow = true;
        if (TryOnFull())
            return;
        SpawnVegetables();
    }

    private bool TryOnFull()
    {
        return _vegetables.All(vegetable => vegetable != null);
    }

    public Vegetable GetVegetable()
    {
        if (_isBusy)
            return null; 
        
        var vegetable = _vegetables.FirstOrDefault(v => v != null);
        if (vegetable != null)
        {
            _vegetables[vegetable.CurrentIndex] = null;
            Busy();
        }

        return vegetable;
    }

    private async void Busy()
    {
        _isBusy = true;
        await Task.Delay(_timeBusy);
        SpawnVegetables();
        _isBusy = false;
    }
}
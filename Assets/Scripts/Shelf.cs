using System.Threading.Tasks;
using UnityEngine;

public class Shelf : MonoBehaviour
{
    [SerializeField] private ItemsPack _itemsPack;
    [SerializeField] private int _timeBusy;
        
    private bool _isBusy;

    public Vegetable GetVegetable()
    {
        if (_isBusy)
            return null;

        var vegetable = _itemsPack.GiveItem();
        
        return vegetable;
    }
        
    public void TakeVegetable(ItemsPack itemsPack)
    {
        if (_itemsPack.IsFull || _isBusy)
            return;
            
        var vegetable = itemsPack.GiveItem();
        if(vegetable == null)
            return;

        vegetable.transform.parent = null;
        _itemsPack.Add(vegetable);
        vegetable.FlyTo(_itemsPack);
        Busy();
    }

    private async void Busy()
    {
        _isBusy = true;
        await Task.Delay(_timeBusy);
        _isBusy = false;
    }
}
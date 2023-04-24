using System.Threading.Tasks;
using UnityEngine;

public class CashRegister : MonoBehaviour
{
    [SerializeField] private Collider _cashierPoint;
    [SerializeField] private Collider _buyerPoint;
    [SerializeField] private ItemsPack _itemsPack;
    [SerializeField] private ItemsPack _scannedPack;
    [SerializeField] private ItemsPack _cashPack;
    [SerializeField] private int _timeBusy;
        
    private bool _isBusy;

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

    public void Buy()
    {
        Buying();
    }
    
    private void Buying()
    {
        if (_isBusy)
            return;
            
        var item = _itemsPack.GiveItem();
        if(item == null)
            return;

        _scannedPack.Add(item);
        item.FlyTo(_scannedPack);
        Busy();
    }
}
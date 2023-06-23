using System;
using System.Threading.Tasks;
using UnityEngine;

public class CashDesk : MonoBehaviour
{
    [SerializeField] private Collider _cashierPoint;
    [SerializeField] private Collider _buyerPoint;
    [SerializeField] private ItemsPack _itemsPack;
    [SerializeField] private ItemsPack _scannedPack;
    [SerializeField] private ItemsPack _cashPack;
    [SerializeField] private int _timeBusy;
    [field: SerializeField] public Transform TargetBuyer { get; private set; }

    private bool _isBusy;
    private int _currentBuyCount;
    private Buyer _buyer;

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
        _currentBuyCount--;
        if (_currentBuyCount == 0)
            _buyer.Payment();
        Busy();
    }

    internal void VisitBuyer(Buyer buyer, int count)
    {
        if (_currentBuyCount > 0)
            return;
        
        _currentBuyCount = count;
        _buyer = buyer;
    }
}
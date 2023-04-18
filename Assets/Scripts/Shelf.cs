using System.Threading.Tasks;
using UnityEngine;

namespace DefaultNamespace
{
    public class Shelf : MonoBehaviour
    {
        [SerializeField] private ItemsPack _itemsPack;
        
        private bool _isBusy;
        private int _timeBusy;

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
            
            // vegetable.transform.SetParent(_itemsPack.transform);
            vegetable.CurrentIndex = _itemsPack.Count;
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
}
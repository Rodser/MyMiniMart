using UnityEngine;

namespace Hero
{
    [RequireComponent(typeof(Rigidbody))]
    public class HeroInterplay : MonoBehaviour
    {
        private HeroAnimator _heroAnimator;
        private ItemsPack _itemsPack;

        public void Construct(ItemsPack itemsPack, HeroAnimator animator)
        {
            _itemsPack = itemsPack;
            _heroAnimator = animator;
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.TryGetComponent(out Plant plant))
            {
                TakeItem(plant);
            }
            else if(other.TryGetComponent(out Shelf shelf))
            {
                GiveItem(shelf);
            }
            else if (other.TryGetComponent(out CashRegister cashRegister))
            {
                cashRegister.Buy();
            }
        }

        private void GiveItem(Shelf shelf)
        {
            shelf.TakeItem(_itemsPack);
            if(_itemsPack.Count == 0)
                _heroAnimator.HasCargo(false);
        }

        private void TakeItem(Plant plant)
        {
            if(_itemsPack.IsFull)
                return;
            var item = plant.GetVegetable();
            if (item == null)
                return;
            item.FlyTo(_itemsPack);
            _itemsPack.Add(item);
            _heroAnimator.HasCargo(true);
        }
    }
}
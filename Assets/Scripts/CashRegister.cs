using UnityEngine;

namespace DefaultNamespace
{
    public class CashRegister : MonoBehaviour
    {
        [SerializeField] private Collider _cashierPoint;
        [SerializeField] private Collider _buyerPoint;
        [SerializeField] private ItemsPack _itemsPack;
        [SerializeField] private int _timeBusy;

        
    }
}
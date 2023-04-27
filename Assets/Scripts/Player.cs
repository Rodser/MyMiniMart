using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _turnSpeed;
    [SerializeField] private float _sellTime;
    [SerializeField] private float _coinTime;
    [SerializeField] private ItemsPack _itemsPack;
    
    private static readonly int MoveValue = Animator.StringToHash("Move");
    private static readonly int IsCarry = Animator.StringToHash("Carry");
    private Rigidbody _ridigbody;
    private Animator _animator;
    private Mover _mover;
    private bool _sellBlocks;
    private int _coinsToSpawn = 0;
        
    public Rigidbody Ridigbody => _ridigbody;
    public float MoveSpeed => _moveSpeed;
    public float TurnSpeed => _turnSpeed;

    public  void MoveAnim(float strength)
    {
        _animator.SetFloat(MoveValue, strength);
    }

    private void Awake()
    {
        _ridigbody = GetComponent<Rigidbody>();
        _animator = GetComponentInChildren<Animator>();
        _sellBlocks = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out Plant plant))
        {
            TakeVegetable(plant);
        }
        else if(other.TryGetComponent(out Shelf shelf))
        {
            GiveVegetable(shelf);
        }
        else if (other.TryGetComponent(out CashRegister cashRegister))
        {
            cashRegister.Buy();
        }
    }

    private void GiveVegetable(Shelf shelf)
    {
        shelf.TakeVegetable(_itemsPack);
        if(_itemsPack.Count == 0)
            SetCarryAnim(false);
    }

    private void TakeVegetable(Plant plant)
    {
        if(_itemsPack.IsFull)
            return;
        var item = plant.GetVegetable();
        if (item == null)
            return;
        item.FlyTo(_itemsPack);
        _itemsPack.Add(item);
        SetCarryAnim(true);
    }

    private void SetCarryAnim(bool value)
    {
        _animator.SetBool(IsCarry, value);
    }
}
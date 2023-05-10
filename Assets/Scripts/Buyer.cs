using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
internal class Buyer : MonoBehaviour
{
    [SerializeField] private ItemsPack _itemsPack;
    
    private static readonly int MoveValue = Animator.StringToHash("Move");
    private static readonly int IsCarry = Animator.StringToHash("Carry");
    private Vector3 _shelfPosition;
    private NavMeshAgent _agent;
    private PersonState _state;
    private Animator _animator;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponentInChildren<Animator>();
        _state = PersonState.Wait;
        Debug.Log(_agent.acceleration); 
    }

    private void Update()
    {
        MoveAnim(_agent.velocity.magnitude);
    }

    public void Construct()
    {
        SetCarryAnim(false);
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out CashRegister cashRegister))
        {
            GiveVegetable(cashRegister);
        }
        else if(other.TryGetComponent(out Shelf shelf))
        {
            TakeVegetable(shelf);
        }
    }

    private void GiveVegetable(CashRegister cashRegister)
    {
        cashRegister.TakeVegetable(_itemsPack);
        if(_itemsPack.Count == 0)
            SetCarryAnim(false);
    }

    private void TakeVegetable(Shelf shelf)
    {
        if (_itemsPack.IsFull)
        {
            return;
        }
        var item = shelf.GetVegetable();
        if (item == null)
            return;
        item.FlyTo(_itemsPack);
        _itemsPack.Add(item);
        SetCarryAnim(true);
    }

    private void ChangeState(PersonState personState, Vector3 targetPosition)
    {
        switch (personState)
        {
            case PersonState.Picking:
                Move(targetPosition);
                break;
            case PersonState.Wait:
                break;
            case PersonState.Buy:
                Move(targetPosition);
                break;
            case PersonState.Away:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(personState), personState, null);
        }
    }

    private void Move(Vector3 position)
    {
        _agent.destination = position;
    }
    
    private  void MoveAnim(float strength)
    {
        _animator.SetFloat(MoveValue, strength);
    }
    
    private void SetCarryAnim(bool value)
    {
        _animator.SetBool(IsCarry, value);
    }
}
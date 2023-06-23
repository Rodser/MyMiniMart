using Hero;
using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
internal class Buyer : MonoBehaviour
{
    [SerializeField] private ItemsPack _itemsPack;
    
    private NavMeshAgent _agent;
    private MoveAnimator _moveAnimator;
    
    private PersonState _state;

    private Vector3 _shelfTarget;
    private Vector3 _cashDeskTarget;
    private Vector3 _awayTarget;
    private bool _isCollect;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _state = PersonState.Wait;
    }

    private void Update()
    {
        _moveAnimator.Move(_agent.velocity.magnitude);
        if (_isCollect)
        {
            ChangeState(PersonState.Buy, _cashDeskTarget);
        }
    }

    public void Construct(MoveAnimator animator, Vector3 shelfTarget, Vector3 cashDeskTarget, Vector3 awayTarget)
    {
        _moveAnimator = animator;
        _shelfTarget = shelfTarget;
        _cashDeskTarget = cashDeskTarget;
        _awayTarget = awayTarget;
        _moveAnimator.HasCargo(false);
        ChangeState(PersonState.Picking, _shelfTarget);
    }

    internal void Payment()
    {
        Debug.Log("Has bought");
        _isCollect = false;
        ChangeState(PersonState.Away, _awayTarget);
    }

    public void ChangeState(PersonState personState, Vector3 targetPosition)
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
                Move(targetPosition);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(personState), personState, null);
        }
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out CashDesk cashDesk))
        {
            cashDesk.VisitBuyer(this, _itemsPack.Count);
            GiveVegetable(cashDesk);
        }
        else if(other.TryGetComponent(out Shelf shelf))
        {
            TakeVegetable(shelf);
        }
    }

    private void GiveVegetable(CashDesk cashDesk)
    {
        cashDesk.TakeVegetable(_itemsPack);
        if (_itemsPack.Count == 0)
        {
            _moveAnimator.HasCargo(false);
        }
    }

    private void TakeVegetable(Shelf shelf)
    {
        if (_itemsPack.IsFull)
        {
            _isCollect = true;
            return;
        }
        var item = shelf.GetVegetable();
        if (item == null)
            return;
        item.FlyTo(_itemsPack);
        _itemsPack.Add(item);
        _moveAnimator.HasCargo(true);
    }

    private void Move(Vector3 position)
    {
        _agent.destination = position;
    }
}
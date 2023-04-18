using System.Collections.Generic;
using UnityEngine;

enum PlayerState
{
    Idle = 0,
    Carry = 1,
}
    
[RequireComponent(typeof(Mover), typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _turnSpeed;
    [SerializeField] private float _sellTime;
    [SerializeField] private float _coinTime;
    [SerializeField] private Transform _stackPoint;
    
    private Stack<Transform> _stackedBlocks;
    private bool _isMoving = false;
    private Vector3 _movePosition;
    private Rigidbody _ridigbody;
    private Animator _animator;
    private PlayerState _state;
    private bool _sellBlocks;
    private int _coinsToSpawn = 0;

    private Mover _mover;
    private Joystick _joystick;
    private static readonly int MoveValue = Animator.StringToHash("Move");
    private static readonly int IsCarry = Animator.StringToHash("Carry");
    private float _timeLastMove;
    private Vector2 _startTouchPosition;

    private InputPlayerSystem _inputPlayer;
    
    public Rigidbody Ridigbody => _ridigbody;
    public float MoveSpeed => _moveSpeed;
    public float TurnSpeed => _turnSpeed;
    public Joystick Joystick => _joystick;

    public void Construct(InputPlayerSystem inputPlayer, Joystick joystick)
    {
        _inputPlayer = inputPlayer;
        _joystick = joystick;
    }

    public  void MoveAnim(float strength)
    {
        _animator.SetFloat(MoveValue, strength);
    }

    private void Awake()
    {
        _ridigbody = GetComponent<Rigidbody>();
        _animator = GetComponentInChildren<Animator>();
        _state = PlayerState.Idle;
        _stackedBlocks = new Stack<Transform>();
        _sellBlocks = false;
    }

    private void Start()
    {
        _mover = GetComponent<Mover>();
        _mover.Construct(_inputPlayer.MoverActionMap.Move, this);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Plant"))
        {
            TakeVegetable(other);
        }
    }

    private void TakeVegetable(Collider other)
    {
        var item = other.GetComponent<Plant>().GetVegetable();
        if (item == null)
            return;
        item.FlyTo(_stackPoint);
        SetCarryAnim(true);
    }

    private void SetCarryAnim(bool value)
    {
        _animator.SetBool(IsCarry, value);
    }
}
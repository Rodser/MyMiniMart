using System;
using System.Collections.Generic;
using _Ollie.Scripts;
using Cinemachine;
using UnityEngine;

enum PlayerState
{
    Idle = 0,
    IdleCarry = 1,
    Moving = 2,
    MovingCarry = 3
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
    private Transform _model;
    private const float _delta = 0.001f;
    private PlayerState _state;
    private bool _sellBlocks;
    private int _coinsToSpawn = 0;
        
        
    [SerializeField] private float _backlash = 0.01f;

    private Mover _mover;
    private static readonly int MoveValue = Animator.StringToHash("Move");
    private static readonly int IsCarry = Animator.StringToHash("Carry");

    private void Awake()
    {
        _ridigbody = GetComponent<Rigidbody>();
        _animator = GetComponentInChildren<Animator>();
        _model = _animator.gameObject.transform;
        _state = PlayerState.Idle;
        _stackedBlocks = new Stack<Transform>();
        _sellBlocks = false;
    }

    private void Start()
    {
        _mover = GetComponent<Mover>();
    }

    private void FixedUpdate()
    {
        if (_isMoving)
        {
            _ridigbody.velocity = _movePosition * _moveSpeed;
            _model.rotation = Quaternion.Slerp(_model.rotation, Quaternion.LookRotation(_movePosition), _turnSpeed * Time.fixedDeltaTime);
            _isMoving = false;
        }
    }

        
    public void Move(Vector3 newPosition)
    { 
        _movePosition = new Vector3(newPosition.x, 0, newPosition.y);
        if (!Mathf.Approximately(_movePosition.sqrMagnitude, 0))
        {
            MoveAnim(_movePosition.sqrMagnitude);
            _isMoving = true;
        }
        Debug.Log("Move pos: " + _movePosition);     
    }
        
    private void MoveAnim(float strength)
    {
        _state = PlayerState.Moving;
        _animator.SetFloat(MoveValue, strength);
    }

    private void SetCarryAnim(bool value)
    {
        _animator.SetBool(IsCarry, value);
    }

    private void StopAnim()
    {
        _state = PlayerState.Idle;
    }
        
}
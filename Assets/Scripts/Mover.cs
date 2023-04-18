using UnityEngine;
using UnityEngine.InputSystem;

public class Mover : MonoBehaviour
{
    private Joystick _joystick;
    private InputAction _inputMove;
    private float _moveSpeed;
    private float _speedRotate;
    private Player _parent;
    private Vector2 _position;
    private Vector2 _delta;
    private Rigidbody _playerRigidbody;

    public void Construct(InputAction inputMove, Player player)
    {
        _inputMove = inputMove;
        _moveSpeed = player.MoveSpeed;
        _speedRotate = player.TurnSpeed;
        _parent = player;
        _playerRigidbody = player.Ridigbody;
        _joystick = player.Joystick;
        Enable();
    }

    private void Enable()
    {
        _inputMove.Enable();
    }

    private void FixedUpdate()
    {
        if(_joystick is null)
            return;
        
        Vector3 direction = new Vector3(_joystick.Horizontal, 0f, _joystick.Vertical);
        Move(-direction);
    }

    private void Move(Vector3 move)
    {
        var strength = Mathf.Clamp01(move.sqrMagnitude);
        _parent.MoveAnim(strength);
        
        move *= _moveSpeed * Time.fixedDeltaTime;
        var direction = Vector3.RotateTowards(
            _playerRigidbody.transform.forward, 
            move, 
            _speedRotate * Time.fixedDeltaTime, 
            0f);

        _playerRigidbody.MovePosition(_playerRigidbody.position + move);
        _playerRigidbody.MoveRotation(Quaternion.LookRotation(direction));
    }
}
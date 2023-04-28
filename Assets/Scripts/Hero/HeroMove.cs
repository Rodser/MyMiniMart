using Infrastructure;
using Infrastructure.Services;
using Infrastructure.Services.Input;
using UnityEngine;

namespace Hero
{
    public class HeroMove : MonoBehaviour
    {
        [SerializeField] private Player _player;
        
        private Rigidbody _playerRigidbody;
        private IInputService _inputService;
        private float _moveSpeed;
        private float _speedRotate;

        private void Start()
        {
            _inputService = AllServices.Container.Single<IInputService>();
            _playerRigidbody = _player.Ridigbody;
            _moveSpeed = _player.MoveSpeed;
            _speedRotate = _player.TurnSpeed;
        }

        private void FixedUpdate()
        {
            var direction = new Vector3(_inputService.Axis.x, 0f, _inputService.Axis.y);
            Move(-direction);
        }

        private void Move(Vector3 move)
        {
            var strength = Mathf.Clamp01(move.sqrMagnitude);
            _player.MoveAnim(strength);
        
            move *= _moveSpeed * Time.fixedDeltaTime;
            _playerRigidbody.MovePosition(_playerRigidbody.position + move);
            
            var direction = Vector3.RotateTowards(
                _playerRigidbody.transform.forward, 
                move, 
                _speedRotate * Time.fixedDeltaTime, 
                0f);
            _playerRigidbody.MoveRotation(Quaternion.LookRotation(direction));
        }
    }
}
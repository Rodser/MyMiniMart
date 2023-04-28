using Data;
using Infrastructure.Services;
using Infrastructure.Services.Input;
using Infrastructure.Services.PersistentProgress;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Hero
{
    public class HeroMove : MonoBehaviour, ISaveProgress
    {
        private Player _player;
        private Rigidbody _playerRigidbody;
        private IInputService _inputService;
        private float _moveSpeed;
        private float _speedRotate;

        private void Awake()
        {
            _inputService = AllServices.Container.Single<IInputService>();
            _player = GetComponent<Player>();
            _playerRigidbody = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            _moveSpeed = _player.MoveSpeed;
            _speedRotate = _player.TurnSpeed;
        }

        private void FixedUpdate()
        {
            var direction = new Vector3(_inputService.Axis.x, 0f, _inputService.Axis.y);
            Move(-direction);
        }

        public void LoadProgress(PlayerProgress progress)
        {
            if (GetCurrentLevel() != progress.WorldData.PositionOnLevel.Level)
                return;
            var position = progress.WorldData.PositionOnLevel.Position.AsVector3();
            _playerRigidbody.position = position;
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.WorldData.PositionOnLevel = new PositionOnLevel(GetCurrentLevel(), _playerRigidbody.position.AsVector3Data());
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

        private static string GetCurrentLevel()
        {
            return SceneManager.GetActiveScene().name;
        }
    }
}
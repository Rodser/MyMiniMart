using Data;
using Infrastructure.Services.Input;
using Infrastructure.Services.PersistentProgress;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Hero
{
    public class HeroMove : MonoBehaviour, ISaveProgress
    {
        private Rigidbody _playerRigidbody;
        private IInputService _inputService;
        private float _moveSpeed;
        private float _speedRotate;
        private HeroAnimator _heroAnimator;

        public void Construct(HeroConfig config, HeroAnimator animator, IInputService inputService)
        {
            _moveSpeed = config.MoveSpeed;
            _speedRotate = config.RotateSpeed;
            _inputService = inputService;
            _heroAnimator = animator;
            _playerRigidbody = GetComponent<Rigidbody>();
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
            Debug.Log(position);
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.WorldData.PositionOnLevel = new PositionOnLevel(GetCurrentLevel(), _playerRigidbody.position.AsVector3Data());
        }

        private void Move(Vector3 move)
        {
            var strength = Mathf.Clamp01(move.sqrMagnitude);
            _heroAnimator.Move(strength);
        
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
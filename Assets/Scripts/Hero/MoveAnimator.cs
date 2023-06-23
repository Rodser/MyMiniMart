using UnityEngine;

namespace Hero
{
    public class MoveAnimator : MonoBehaviour
    {
        private static readonly int MoveValue = Animator.StringToHash("Move");
        private static readonly int IsCarry = Animator.StringToHash("Carry");
        
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponentInChildren<Animator>();
        }

        public  void Move(float strength)
        {
            _animator.SetFloat(MoveValue, strength);
        }
        
        public void HasCargo(bool value)
        {
            _animator.SetBool(IsCarry, value);
        }
    }
}
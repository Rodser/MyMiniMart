using UnityEngine;

namespace Logic
{
    public class CameraFollow : MonoBehaviour
    {
        private Transform _target;

        public void Construct(Transform target)
        {
            _target = target;
        }

        private void FixedUpdate()
        {
            if(_target is null)
                return;
            transform.position = _target.transform.position;
        }
    }
}
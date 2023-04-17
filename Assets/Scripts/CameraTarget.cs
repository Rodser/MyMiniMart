using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    private Transform _target;

    public void Construct(Transform target)
    {
        _target = target;
    }

    private void Update()
    {
        if(_target is null)
            return;
        transform.position = _target.transform.position;
    }
}
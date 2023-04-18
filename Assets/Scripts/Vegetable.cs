using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Vegetable : MonoBehaviour
{
    [SerializeField] private float _speed;
    private float _time;

    public async void FlyTo(Transform target)
    {
        var position = transform.position;
        var targetPosition = target.position;

        var up = Vector3.Lerp(position, targetPosition, 0.5f);
        up.y += 1f;
        await Fly(position, up ,target);
        transform.SetParent(target);
    }

    private async Task Fly(Vector3 startPosition, Vector3 up, Transform target)
    {
        Debug.Log(startPosition);
        Debug.Log(target.position);
        
        while (_time < 1)
        {
            await Task.Yield();
            _time += _speed * Time.deltaTime;
            var targetPosition = target.position;      
            Debug.Log(targetPosition);
            transform.position = GetBezier(startPosition, up, targetPosition, _time);
        }
    }

    private static Vector3 GetBezier(Vector3 point0, Vector3 point1, Vector3 point2, float time)
    {
        Vector3 point01 = Vector3.Lerp(point0, point1, time);
        Vector3 point02 = Vector3.Lerp(point1, point2, time);

        Vector3 point12 = Vector3.Lerp(point01, point02, time);

        return point12;
    }
}
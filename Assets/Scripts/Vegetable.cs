using System.Threading.Tasks;
using UnityEngine;

public class Vegetable : MonoBehaviour
{
    [SerializeField] private float _speed;
    private float _time;
    private bool _isFly;
    public int CurrentIndex { get;  set; }

    public async void FlyTo(ItemsPack pack)
    {
        var position = transform.position;

        var up = Vector3.Lerp(position, pack.GetPosition(CurrentIndex), 0.5f);
        up.y += 1f;
        await Fly(position, up ,pack);
        transform.SetParent(pack.transform);
    }

    private async Task Fly(Vector3 startPosition, Vector3 up, ItemsPack pack)
    {
        _isFly = true;
        while (_time < 1)
        {
            await Task.Yield();
            _time += _speed * Time.deltaTime;    
            transform.position = GetBezier(startPosition, up, pack.GetPosition(CurrentIndex), _time);
        }

        _isFly = false;
    }

    private static Vector3 GetBezier(Vector3 point0, Vector3 point1, Vector3 point2, float time)
    {
        Vector3 point01 = Vector3.Lerp(point0, point1, time);
        Vector3 point02 = Vector3.Lerp(point1, point2, time);

        Vector3 point12 = Vector3.Lerp(point01, point02, time);

        return point12;
    }
}
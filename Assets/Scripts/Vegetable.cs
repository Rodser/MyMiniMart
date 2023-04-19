using System.Threading.Tasks;
using UnityEngine;

public class Vegetable : MonoBehaviour
{
    [SerializeField] private float _speedFlying;

    private float _time;

    public int CurrentIndex { get;  set; }

    public async void FlyTo(ItemsPack pack)
    {
        var position = transform.position;
        var up = Vector3.Lerp(position, pack.GetPosition(CurrentIndex), 0.5f);
        up.y += 1f;
        await Fly(position, up ,pack);
        transform.SetParent(pack.StartItemPoint);
    }

    private async Task Fly(Vector3 startPosition, Vector3 upPosition, ItemsPack pack)
    {
        _time = 0f;
        while (_time < 1)
        {
            await Task.Yield();
            _time += _speedFlying * Time.deltaTime;
            transform.position = GetCurve(startPosition, upPosition, pack.GetPosition(CurrentIndex), _time);
        }
    }

    private static Vector3 GetCurve(Vector3 point0, Vector3 point1, Vector3 point2, float time)
    {
        Vector3 point01 = Vector3.Lerp(point0, point1, time);
        Vector3 point02 = Vector3.Lerp(point1, point2, time);

        Vector3 point12 = Vector3.Lerp(point01, point02, time);

        return point12;
    }
}
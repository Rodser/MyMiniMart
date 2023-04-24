using System;
using UnityEngine;

public class Zone : MonoBehaviour
{
    [Header("Контрольные точки")]
    [SerializeField] private Transform _pointPlayer;
    [SerializeField] private Transform _pointCashDesk;
    [SerializeField] private Transform _pointShelf;
    [SerializeField] private Transform _pointBed;
    [SerializeField] private Transform _pointBuyer;
    [SerializeField] private Transform[] _points;
    
    public Transform PointPlayer => _pointPlayer;
    public Transform PointBuyer => _pointBuyer;
    public Transform PointShelf => _pointShelf;
    public Transform PointCashDesk => _pointCashDesk;

    public void StartSpawn(GameObject cashDesk, GameObject shelf, GameObject plant)
    {
        if (plant == null) throw new ArgumentNullException(nameof(plant));
        Spawn(_pointCashDesk.position, cashDesk);
        Spawn(_pointShelf.position, shelf);
        Spawn(_pointBed.position, plant);
    }

    private GameObject Spawn(Vector3 position, GameObject item)
    {
        return Instantiate(item, position, Quaternion.identity);
    }
}
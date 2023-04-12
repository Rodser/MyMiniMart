using System;
using JetBrains.Annotations;
using UnityEngine;

public class Zone : MonoBehaviour
{
    [Header("Контрольные точки")]
    [SerializeField] private Transform _pointPlayer;
    [SerializeField] private Transform _pointCashDesk;
    [SerializeField] private Transform _pointShelf;
    [SerializeField] private Transform _pointBed;
    [SerializeField] private Transform[] _points;


    public Transform PointPlayer => _pointPlayer;
        
        
    public void StartSpawn(GameObject cashDesk, GameObject shelf, [NotNull] GameObject bed)
    {
        if (bed == null) throw new ArgumentNullException(nameof(bed));
        Spawn(_pointCashDesk.position, cashDesk);
        Spawn(_pointShelf.position, shelf);
        Spawn(_pointBed.position, bed);
    }

    private GameObject Spawn(Vector3 position, GameObject item)
    {
        return Instantiate(item, position, Quaternion.identity);
    }
}
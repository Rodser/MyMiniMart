using Logic;
using UnityEngine;

internal class BuyerSpawner : MonoBehaviour, ISpawner
{
    private readonly Buyer _prefab;

    public BuyerSpawner(Buyer prefab)
    {
        _prefab = prefab;
    }
    
    public void Spawn()
    {
        // TODO: нужна фабрика
        var buyer = Object.Instantiate(_prefab, transform.position, Quaternion.identity);
    }
}
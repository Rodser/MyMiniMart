using UnityEngine;

internal class BuyerSpawner
{
    private readonly Transform _zonePointBuyer;
    private readonly Buyer _buyerPrefab;
    private readonly Zone _zone;

    public BuyerSpawner(Transform zonePointBuyer, Buyer buyerPrefab, Zone zone)
    {
        _zonePointBuyer = zonePointBuyer;
        _buyerPrefab = buyerPrefab;
        _zone = zone;
        Spawner();
    }

    private void Spawner()
    {
        var buyer = Object.Instantiate(_buyerPrefab, _zonePointBuyer.position, Quaternion.identity);
        buyer.Construct(_zone);
    }
}
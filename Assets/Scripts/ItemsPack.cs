using System.Collections.Generic;
using UnityEngine;

public class ItemsPack : MonoBehaviour
{
    [SerializeField] private int _maxItems;
    [SerializeField] private float _itemShift;

    private List<Vegetable> _Items;

    public bool IsFull => _Items.Count == _maxItems;
    public int Count { get; private set; }

    private void Awake()
    {
        _Items = new List<Vegetable>();
    }

    public Vector3 GetPosition(int index)
    {
        var position = transform.position;
        position.y += index * _itemShift;
        return position;
    }

    public void Add(Vegetable item)
    {
        item.CurrentIndex = Count;
        _Items.Add(item);
        Count++;
    }

    public Vegetable GiveItem()
    {
        Vegetable item = null;
        
        if(_Items.Count > 0)
            item = _Items[^1];
        
        if (item != null)
        {
            _Items.Remove(item);
        }

        return item;
    }
}
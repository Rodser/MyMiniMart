using System.Collections.Generic;
using UnityEngine;

public class ItemsPack : MonoBehaviour
{
    [SerializeField] private Transform _startItemPoint;
    [Space(10)]
    [SerializeField] private int _maxItems;
    [SerializeField] private float _itemShift;
    [Space(10)]
    [SerializeField] private bool _inBox;
    [SerializeField, HideInInspector] private int _itemCountX;
    [SerializeField, HideInInspector] private int _itemCountY;
    [SerializeField, HideInInspector] private int _itemCountZ;

    private List<Vegetable> _Items;

    public Transform StartItemPoint => _startItemPoint;
    public bool IsFull => _Items.Count == MaxItems;
    public int Count => _Items.Count;
    public bool InBox => _inBox;
    public float ItemShift => _itemShift;
    public int MaxItems => _maxItems;
    public int ItemCountX => _itemCountX;
    public int ItemCountY => _itemCountY;
    public int ItemCountZ => _itemCountZ;

    private void Awake()
    {
        _Items = new List<Vegetable>();
    }

    public Vector3 GetPosition(int index)
    {
        if (InBox)
        {
            var position = FindEmptyPosition(index);
            return position;
        }
        else
        {
            var position = transform.position;
            position.y += index * ItemShift;
            return position;
        }
    }

    public void Add(Vegetable item)
    {
        item.CurrentIndex = Count;
        Debug.Log(Count);
        _Items.Add(item);
    }

    public Vegetable GiveItem()
    {
        if (Count == 0) return null;
        
        var item = _Items[^1];

        if (item != null)
        {
            _Items.Remove(item);
        }

        return item;
    }

    private Vector3 FindEmptyPosition(int index)
    {
        var position = _startItemPoint.position;
        var offset = position;

        int x = index % ItemCountX;
        int z = (index / ItemCountX) % ItemCountZ;
        int y = (index / (ItemCountX * ItemCountZ)) % ItemCountY;

        offset.x += x * ItemShift;
        offset.y += y * ItemShift;
        offset.z += z * ItemShift;

        Vector3 rotation = transform.rotation * (offset - position);
        Debug.Log(rotation);
        offset = position + rotation;
        
        return offset;
    }
}
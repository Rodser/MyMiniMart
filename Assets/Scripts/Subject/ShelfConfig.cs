using UnityEngine;

namespace Subject
{
    [CreateAssetMenu(fileName = "Shelf", menuName = "Game/Shelf")]
    public class ShelfConfig : ScriptableObject
    {
        [field: SerializeField] public GameObject Shelf { get; private set; }
        [field: SerializeField] public ItemsPack ItemsPack { get; private set; }
        [field: SerializeField] public int TimeBusy { get; private set; }
    }
}
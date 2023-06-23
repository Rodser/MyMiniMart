using UnityEngine;

namespace Subject
{
    [CreateAssetMenu(fileName = "Buyer", menuName = "Game/Buyer")]
    public class BuyerConfig : ScriptableObject
    {
        [field: SerializeField] public GameObject Buyer { get; private set; }
    }
}
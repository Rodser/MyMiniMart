using UnityEngine;

namespace Subject
{
    [CreateAssetMenu(fileName = "CashDesk", menuName = "Game/CashDesk")]
    public class CashDeskConfig : ScriptableObject
    {
        [field: SerializeField] public GameObject CashDesk { get; private set; }
    }
}
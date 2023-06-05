using UnityEngine;

namespace Hero
{
    [CreateAssetMenu(fileName = "Hero", menuName = "Game/Hero")]
    public class HeroConfig : ScriptableObject
    {
        [field: SerializeField] public GameObject HeroPrefab { get; private set; }
        [field: SerializeField] public float MoveSpeed { get; private set; }
        [field: SerializeField] public float RotateSpeed { get; private set; }
    }
}
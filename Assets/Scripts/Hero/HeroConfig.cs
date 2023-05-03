using UnityEngine;

namespace Hero
{
    [CreateAssetMenu(fileName = "Hero", menuName = "Game/Hero", order = 0)]
    public class HeroConfig : ScriptableObject
    {
        public float MoveSpeed;
        public float RotateSpeed;
    }
}
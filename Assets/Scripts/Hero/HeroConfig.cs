using UnityEngine;

namespace Hero
{
    [CreateAssetMenu(fileName = "Hero", menuName = "Game/Hero")]
    public class HeroConfig : ScriptableObject
    {
        public float MoveSpeed;
        public float RotateSpeed;
    }
}
using UnityEngine;

namespace Infrastructure
{
    public class GameRunner : MonoBehaviour
    {
        [SerializeField] private Bootstrapper _bootstrapper;

        private void Awake()
        {
            var bootstrapper = FindFirstObjectByType<Bootstrapper>();
            if (bootstrapper != null) 
                return;
            Instantiate(_bootstrapper);
        }
    }
}
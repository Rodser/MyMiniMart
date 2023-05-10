using UnityEngine;

namespace Infrastructure
{
    public class GameRunner : MonoBehaviour
    {
        [SerializeField] private Bootstrapper _bootstrapper;

        private void Awake()
        {
            Object bootstrapper = FindObjectOfType(typeof(Bootstrapper));
            if (bootstrapper != null) 
                return;
            Instantiate(_bootstrapper);
        }
    }
}
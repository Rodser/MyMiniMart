using UnityEngine;
using UnityEngine.AI;

namespace _Ollie.Scripts
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Mover : MonoBehaviour
    {
        private NavMeshAgent _agent;

        private void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
        }

        public void Move(Vector3 destination)
        {
            _agent.SetDestination(destination);
        }
    }
}
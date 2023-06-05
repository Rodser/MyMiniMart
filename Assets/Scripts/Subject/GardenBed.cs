using UnityEngine;

namespace Subject
{
    public class GardenBed : MonoBehaviour
    {
        [field: SerializeField] public Transform[] Landings { get; private set; }

        public void StartGrows(Plant plant)
        {
            foreach (Transform landing in Landings)
            {
                Grow(landing, plant);
            }
        }

        private void Grow(Transform point, Plant plant)
        {
            Instantiate(plant, point);
        }
    }
}
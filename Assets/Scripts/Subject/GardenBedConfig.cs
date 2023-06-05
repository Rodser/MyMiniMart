using UnityEngine;

namespace Subject
{
    [CreateAssetMenu(fileName = "GardenBed", menuName = "Game/GardenBed")]
    public class GardenBedConfig : ScriptableObject
    {
        [field: SerializeField] public GameObject GardenBed { get; private set; }
        [field: SerializeField] public Plant Plant { get; private set; }
    }
}
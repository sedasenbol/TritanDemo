using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "CollectibleSpawnSettings", menuName = "ScriptableObjects/CollectibleSpawnSettings", order = 1)]
    public class CollectibleSpawnSettingsScriptableObject : ScriptableObject
    {
        [SerializeField] private int totalSpawnCount = 10;
        [SerializeField] private float spawnHeight = 0f;
        [SerializeField] private float overlapSphereRadiusToCheck = 1f;
        
        public int TotalSpawnCount => totalSpawnCount;
        public float SpawnHeight => spawnHeight;
        public float OverlapSphereRadiusToCheck => overlapSphereRadiusToCheck;
    }
}
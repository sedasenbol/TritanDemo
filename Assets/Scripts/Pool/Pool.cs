using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;

namespace Pool
{
    public abstract class Pool : MonoBehaviour
    {
        [SerializeField] private CollectibleSpawnSettingsScriptableObject collectibleSpawnSettings;
        [SerializeField] private PoolSettingsScriptableObject poolSettings;
        [SerializeField] private Transform containerTransform;
        
        private Queue<GameObject> itemPoolQueue;

        public Transform SpawnFromPool(Vector3 position, Quaternion rotation)
        {
            var objectSpawned = itemPoolQueue.Dequeue();
            objectSpawned.SetActive(true);
        
            var objectSpawnedTransform = objectSpawned.transform;
            objectSpawnedTransform.position = position;
            objectSpawnedTransform.rotation = rotation;
            
            objectSpawnedTransform.SetParent(containerTransform);

            itemPoolQueue.Enqueue(objectSpawned);
            
            return objectSpawnedTransform;
        }

        public void RecycleGameObject(GameObject go)
        {
            go.transform.SetParent(containerTransform);
            go.SetActive(false);
            itemPoolQueue.Enqueue(go);
        }
    
        //Called by LevelManager.cs when a new level loads.
        public void InitializeItemPoolDict()
        {
            var size = Mathf.Max(collectibleSpawnSettings.TotalSpawnCount, poolSettings.PoolSize);
            
            itemPoolQueue = new Queue<GameObject>(size);
            
            InitializeItemPool(size, itemPoolQueue);
        }
   
        private void InitializeItemPool(int poolSize, Queue<GameObject> newItemPool)
        {
            for (var j = 0; j < poolSize; j++)
            {
                var obj = Instantiate(poolSettings.ItemPrefab, new Vector3(0f, -100f, 0f), Quaternion.identity,
                    gameObject.transform).gameObject;
                obj.SetActive(false);
                newItemPool.Enqueue(obj);
            }
        }

        private void OnDisable()
        {
            itemPoolQueue = null;
        }
    }
}
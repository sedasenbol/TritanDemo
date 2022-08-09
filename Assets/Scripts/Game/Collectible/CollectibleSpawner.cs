using System;
using System.Collections;
using System.Collections.Generic;
using GameCore;
using Pool;
using ScriptableObjects;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Collectible
{
    public class CollectibleSpawner : MonoBehaviour
    {
        public static event Action OnSpawningEnded;
        
        [SerializeField] private CollectibleSpawnSettingsScriptableObject collectibleSpawnSettings;
        [SerializeField] private Renderer floorRenderer;

        private int collisionCounter;
        private float spawnPosXMax, spawnPosXMin, spawnPosZMax, spawnPosZMin;
        
        private void OnEnable()
        {
            LevelManager.OnNewLevelLoaded += OnNewLevelLoaded;
        }

        private void OnDisable()
        {
            LevelManager.OnNewLevelLoaded -= OnNewLevelLoaded;
        }

        private void OnNewLevelLoaded()
        {
            SetSpawnPositionVariables();

            for (int i = 0; i < collectibleSpawnSettings.TotalSpawnCount; i++)
            {
                SpawnCollectible();
            }
            
            OnSpawningEnded?.Invoke();
        }

        private void SetSpawnPositionVariables()
        {
            var bounds = floorRenderer.bounds;
            
            spawnPosXMax = bounds.max.x;
            spawnPosXMin = bounds.min.x;

            spawnPosZMax = bounds.max.z;
            spawnPosZMin = bounds.min.z;
        }

        private void SpawnCollectible()
        {
            var randomCollectible = Random.Range(0f, 1f);
            
            var randomSpawnPosX = Random.Range(spawnPosXMin, spawnPosXMax);
            var randomSpawnPosZ = Random.Range(spawnPosZMin, spawnPosZMax);
            var randomSpawnPos = new Vector3(randomSpawnPosX, collectibleSpawnSettings.SpawnHeight, randomSpawnPosZ);

            if (randomCollectible < 0.5f)
            {
                Collectible1Pool.Instance.SpawnFromPool(randomSpawnPos, Quaternion.identity);
            }
            else
            {
                Collectible2Pool.Instance.SpawnFromPool(randomSpawnPos, Quaternion.identity);
            }
        }
    }
}

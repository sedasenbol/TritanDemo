using System;
using System.Collections;
using System.Collections.Generic;
using Pool;
using ScriptableObjects;
using UnityEngine;
using Pool = Pool.Pool;

namespace Game.Collectible
{
    public class CollectibleCollisionDetector : MonoBehaviour
    {
        [SerializeField] private CollectibleSpawnSettingsScriptableObject collectibleSpawnSettings;
        
        private const int COLLIDER_ARRAY_LENGTH = 5;

        private Transform myTransform;
        private bool checkOverlapSphere;
        private int collectible1Layer;
        private int collectible2Layer;
        private int playerLayer;
        private List<int> collisionLayers;
        private Collider[] colliders ;
        
        private void OnEnable()
        {
            myTransform = transform;
            colliders = new Collider[COLLIDER_ARRAY_LENGTH];

            collectible1Layer = LayerMask.NameToLayer("Collectible1");
            collectible2Layer = LayerMask.NameToLayer("Collectible2");
            playerLayer = LayerMask.NameToLayer("Player");

            collisionLayers = new List<int>() { collectible1Layer, collectible2Layer, playerLayer };

            checkOverlapSphere = true;

            CollectibleSpawner.OnSpawningEnded += OnSpawningEnded;
        }

        private void OnDisable()
        {
            myTransform = null;
            colliders = null;
            
            CollectibleSpawner.OnSpawningEnded -= OnSpawningEnded;
        }

        private void OnSpawningEnded()
        {
            StartCoroutine(StopCheckingOverlapSphere());
        }

        private IEnumerator StopCheckingOverlapSphere()
        {
            yield return null;

            checkOverlapSphere = false;
        }

        private void Update()
        {
            if (!checkOverlapSphere) {return;}
            
            CheckOverlapSphere();
        }

        private void CheckOverlapSphere()
        {
            if (Physics.OverlapSphereNonAlloc(myTransform.position, collectibleSpawnSettings.OverlapSphereRadiusToCheck, 
            colliders) == 0) {return;}

            foreach (var hitCollider in colliders)
            {
                if (hitCollider == null || !collisionLayers.Contains(hitCollider.gameObject.layer) || hitCollider.gameObject == 
                gameObject) {continue;}

                CollectibleCounter.Instance.TotalCollectibleCount--;
                CollectibleRecycler.Instance.RecycleGameObject(gameObject);
                break;
            }

            checkOverlapSphere = false;
        }
    }
}
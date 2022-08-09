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
        [SerializeField] private CollectibleType type;
        
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
                
                Debug.Log("1");
                
                switch (type)
                {
                    case CollectibleType.Type1:
                        Collectible1Pool.Instance.RecycleGameObject(gameObject);
                        break;
                    case CollectibleType.Type2:
                        Collectible2Pool.Instance.RecycleGameObject(gameObject);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
}
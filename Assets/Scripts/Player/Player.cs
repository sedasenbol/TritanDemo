using System;
using DG.Tweening;
using GameCore;
using Input;
using ScriptableObjects;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

namespace Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private PlayerMovementSettingsScriptableObject playerMovementSettings;
        [SerializeField] private NavMeshAgent agent;
        [SerializeField] private Animator animator;
        
        private Camera mainCam;
        private Transform myTransform;
        private RaycastHit[] raycastHits;
        
        private int floorLayerMask;
        private bool isRunning;
        private float initialPlayerHeight;
        
        private void OnEnable()
        {
            SetMainCamera();

            myTransform = transform;
            raycastHits = new RaycastHit[5];
            floorLayerMask = 1 << LayerMask.NameToLayer("Floor");

            initialPlayerHeight = myTransform.position.y;
            
            TouchController.OnPlayerTapped += OnPlayerTapped;
        }

        private void OnDisable()
        {
            mainCam = null;
            myTransform = null;
            raycastHits = null;
            
            TouchController.OnPlayerTapped -= OnPlayerTapped;
        }

        private void Update()
        {
            if (!isRunning) {return;}
            
            CheckIfReachedTheDestination();
        }

        private void CheckIfReachedTheDestination()
        {
            if (agent.pathPending) return;
            
            if (agent.remainingDistance > agent.stoppingDistance) return;
                
            if (agent.hasPath && agent.velocity.sqrMagnitude != 0f) return;
                    
            isRunning = false;
            animator.SetTrigger("Idle");
        }

        private void SetMainCamera()
        {
            mainCam = UnityEngine.Camera.main;
            
            if (mainCam != null) {return;}
            
            Debug.LogError("Tag the main camera.");
        }
        
        private void OnPlayerTapped(Vector3 screenPosV3)
        {
            var ray = mainCam.ScreenPointToRay(screenPosV3);

            if (Physics.RaycastNonAlloc(ray, raycastHits, 50f, floorLayerMask) == 0) {return;}

            var destination = new Vector3()
            {
                x = raycastHits[0].point.x,
                y = initialPlayerHeight,
                z = raycastHits[0].point.z
            };
            
            agent.SetDestination(destination);
            
            animator.SetTrigger("Run");
            isRunning = true;
        }
    }
}
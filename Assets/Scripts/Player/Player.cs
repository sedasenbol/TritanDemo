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
        [SerializeField] private NavMeshAgent agent;
        [SerializeField] private Animator animator;
        
        private Camera mainCam;
        private Transform myTransform;
        private RaycastHit[] raycastHits;
        
        private int floorLayerMask;
        private bool isRunning;
        
        private void OnEnable()
        {
            SetMainCamera();

            myTransform = transform;
            raycastHits = new RaycastHit[5];
            floorLayerMask = 1 << LayerMask.NameToLayer("Floor");
            
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
            
            isRunning = false;
            
            animator.ResetTrigger("Run");
            animator.ResetTrigger("Idle");
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

            agent.SetDestination(raycastHits[0].point);
            
            isRunning = true;

            animator.ResetTrigger("Idle");
            animator.ResetTrigger("Run");
            animator.SetTrigger("Run");
        }
    }
}
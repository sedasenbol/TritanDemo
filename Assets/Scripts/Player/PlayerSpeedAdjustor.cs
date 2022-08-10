using System;
using GameCore;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.AI;

namespace Player
{
    public class PlayerSpeedAdjustor : MonoBehaviour
    {
        [SerializeField] private PlayerMovementSettingsScriptableObject playerMovementSettings;

        private NavMeshAgent agent;
        
        private void OnEnable()
        {
            LevelManager.OnNewLevelLoaded += OnNewLevelLoaded;
            LevelManager.OnLevelFailed += OnLevelFailed;
            LevelManager.OnLevelCompleted += OnLevelCompleted;
        }

        private void OnDisable()
        {
            LevelManager.OnNewLevelLoaded += OnNewLevelLoaded;
            LevelManager.OnLevelFailed += OnLevelFailed;
            LevelManager.OnLevelCompleted += OnLevelCompleted;
        }

        private void OnNewLevelLoaded()
        {
            agent = FindObjectOfType<NavMeshAgent>();
        }

        private void OnLevelCompleted()
        {
            agent = null;
        }

        private void OnLevelFailed()
        {
            agent = null;
        }

        public void DecreaseSpeed()
        {
            agent.speed -= Mathf.Max(playerMovementSettings.SpeedChangeAmount, 0f);
        }
        
        public void IncreaseSpeed()
        {
            agent.speed += playerMovementSettings.SpeedChangeAmount;
        }
    }
}
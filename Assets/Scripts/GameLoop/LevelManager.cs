using System;
using System.Collections;
using DG.Tweening;
using Pool;
using ScriptableObjects;
using UnityEngine;


namespace GameCore
{
    public class LevelManager : Singleton<LevelManager>
    {
        public static event Action OnNewLevelLoaded;
        public static event Action OnLevelFailed;
        public static event Action OnLevelCompleted;

        private bool isGameActive;
        
        // Called by GameManager.cs when "Game" scene is loaded. 
        public void HandleNewLevel()
        {
            isGameActive = true;
            
            Collectible1Pool.Instance.InitializeItemPoolDict();
            Collectible2Pool.Instance.InitializeItemPoolDict();
            
            OnNewLevelLoaded?.Invoke();
        }

        public void FailLevel()
        {
            if (!isGameActive) {return;}
            
            isGameActive = false;
            
            DOTween.CompleteAll();
            OnLevelFailed?.Invoke();
        }
        
        public void CompleteLevel()
        {
            if (!isGameActive) {return;}
            
            isGameActive = false;
            
            DOTween.CompleteAll();
            OnLevelCompleted?.Invoke();
        }
    }
}

using System;
using GameCore;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Input
{
    public class TouchController : MonoBehaviour, IPointerDownHandler
    {
        public static event Action<Vector3> OnPlayerTapped;

        private UnityEngine.Camera mainCam;
        private Vector3 lastDragWorldPosition;

        private bool isGameActive;

        private void SetMainCamera()
        {
            mainCam = UnityEngine.Camera.main;
            
            if (mainCam != null) {return;}
            
            Debug.LogError("Tag the main camera.");
        }

        private void OnNewLevelLoaded()
        {
            isGameActive = true;
        }

        private void OnLevelEnded()
        {
            isGameActive = false;
        }
        
        private void OnEnable()
        {
            SetMainCamera();

            LevelManager.OnNewLevelLoaded += OnNewLevelLoaded;
            LevelManager.OnLevelCompleted += OnLevelEnded;
            LevelManager.OnLevelFailed += OnLevelEnded;
        }

        private void OnDisable()
        {
            mainCam = null;
            
            LevelManager.OnNewLevelLoaded -= OnNewLevelLoaded;
            LevelManager.OnLevelCompleted -= OnLevelEnded;
            LevelManager.OnLevelFailed -= OnLevelEnded;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (!isGameActive) {return;}

            var screenPosV2 = eventData.pointerCurrentRaycast.screenPosition;
            var screenPosV3 = new Vector3(screenPosV2.x, screenPosV2.y, mainCam.nearClipPlane);

            OnPlayerTapped?.Invoke(screenPosV3);
        }
    }
}

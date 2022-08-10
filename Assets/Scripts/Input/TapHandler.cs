using System;
using Game.Collectible;
using GameCore;
using UnityEngine;

namespace Input
{
    public class TapHandler : MonoBehaviour, ITapPositionReceiver
    {
        private UnityEngine.Camera mainCam;
        private bool isGameActive;
        private RaycastHit[] raycastHits;
        private int collectiblesLayerMask;

        private void SetMainCamera()
        {
            mainCam = UnityEngine.Camera.main;
            
            if (mainCam != null) {return;}
            
            Debug.LogError("Tag the main camera.");
        }
        
        private void OnEnable()
        {
            SetMainCamera();
            
            raycastHits = new RaycastHit[5];
            
            var collectible1Layer = LayerMask.NameToLayer("Collectible1");
            var collectible2Layer = LayerMask.NameToLayer("Collectible2");

            collectiblesLayerMask = (1 << collectible1Layer) | (1 << collectible2Layer);

            TouchController.OnPlayerTapped += OnPlayerTapped;
        }

        private void OnDisable()
        {
            mainCam = null;
            
            TouchController.OnPlayerTapped -= OnPlayerTapped;
        }

        public void OnPlayerTapped(Vector3 screenPosV3)
        {
            var ray = mainCam.ScreenPointToRay(screenPosV3);

            var resultCount = Physics.RaycastNonAlloc(ray, raycastHits, 25f, collectiblesLayerMask);
            
            for (int i = 0; i < resultCount; i++)
            {
                CollectibleRecycler.Instance.RecycleGameObject(raycastHits[i].collider.gameObject);
                CollectibleCounter.Instance.CollectedCollectibleCount++;
            }
        }
    }
}
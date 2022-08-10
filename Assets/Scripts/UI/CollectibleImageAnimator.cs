using DG.Tweening;
using GameCore;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class CollectibleImageAnimator : MonoBehaviour
    {
        [SerializeField] private CollectibleImageAnimationSettingsScriptableObject collectibleImageAnimationSettings;
        
        private Transform myTransform;
        private Tween tween;

        private Vector3 initialScale;
        
        private void OnEnable()
        {
            myTransform = transform;
            
            initialScale = myTransform.localScale;

            LevelManager.OnNewLevelLoaded += OnNewLevelLoaded;
        }

        private void OnDisable()
        {
            tween.Complete();
            myTransform.DOComplete();
            myTransform = null;
            
            LevelManager.OnNewLevelLoaded -= OnNewLevelLoaded;
        }

        private void OnNewLevelLoaded()
        {
            myTransform.localScale = initialScale;
        }

        public void Animate()
        {
            myTransform.DOComplete();
            
            tween = myTransform
                .DOScale(initialScale * collectibleImageAnimationSettings.ScaleBy,
                    collectibleImageAnimationSettings.TotalAnimationDuration / 2f).OnComplete(() =>
                    myTransform.DOScale(initialScale, collectibleImageAnimationSettings.TotalAnimationDuration / 2f));

            tween.SetEase(collectibleImageAnimationSettings.AnimationEase);
        }
    }
}
using System;
using DG.Tweening;
using GameCore;
using ScriptableObjects;
using UnityEngine;

namespace UI
{
    public class SpeedButtonAnimator : MonoBehaviour
    {
        [SerializeField] private SpeedButtonAnimationSettingsScriptableObject speedButtonAnimationSettings;
        
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
                .DOScale(initialScale * speedButtonAnimationSettings.ScaleBy,
                    speedButtonAnimationSettings.TotalAnimationDuration / 2f).OnComplete(() =>
                    myTransform.DOScale(initialScale, speedButtonAnimationSettings.TotalAnimationDuration / 2f));

            tween.SetEase(speedButtonAnimationSettings.AnimationEase);
        }
    }
}
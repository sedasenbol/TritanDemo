using System;
using DG.Tweening;
using ScriptableObjects;
using UnityEngine;

namespace UI
{
    public class SpeedButtonAnimator : MonoBehaviour
    {
        [SerializeField] private SpeedButtonAnimationSettingsScriptableObject speedButtonAnimationSettings;
        
        private Transform myTransform;
        private Tween tween;
        
        private void OnEnable()
        {
            myTransform = transform;
        }

        private void OnDisable()
        {
            tween.Complete();
            myTransform.DOComplete();
            myTransform = null;
        }

        public void Animate()
        {
            myTransform.DOComplete();
            
            var initialScale = myTransform.localScale;

            tween = myTransform
                .DOScale(initialScale * speedButtonAnimationSettings.ScaleBy,
                    speedButtonAnimationSettings.TotalAnimationDuration / 2f).OnComplete(() =>
                    myTransform.DOScale(initialScale, speedButtonAnimationSettings.TotalAnimationDuration / 2f));

            tween.SetEase(Ease.InOutSine);
        }
    }
}
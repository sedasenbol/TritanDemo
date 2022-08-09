using System.Collections;
using DG.Tweening;
using GameCore;
using ScriptableObjects;
using UnityEngine;

namespace Game.Collectible
{
    public class CollectibleAnimator : MonoBehaviour
    {
        [SerializeField] private CollectibleAnimationSettingsScriptableObject collectibleAnimationSettings;
        
        private Sequence sequence;
        private Transform myTransform;

        private void OnEnable()
        {
            myTransform = transform;

            StartCoroutine(StartAnimationWithDelay());
        }

        private void OnDisable()
        {
            sequence.Kill();
            myTransform.DOKill();
            
            StopCoroutine(StartAnimationWithDelay());
            
            myTransform = null;
        }

        private IEnumerator StartAnimationWithDelay()
        {
            yield return null;
            
            StartAnimation();
        }
        
        private void StartAnimation()
        {
            sequence = DOTween.Sequence();

            sequence.Append(myTransform.DOBlendableRotateBy(collectibleAnimationSettings.RotateBy,
                collectibleAnimationSettings.AnimationDuration, RotateMode.WorldAxisAdd));

            var initialPos = myTransform.position;

            sequence.Join(myTransform
                .DOMove(initialPos + collectibleAnimationSettings.MoveBy, collectibleAnimationSettings.AnimationDuration / 2f)
                .OnComplete(() => myTransform.DOMove(initialPos, collectibleAnimationSettings.AnimationDuration / 2f)));

            var initialScale = myTransform.lossyScale;

            sequence.Join(myTransform
                .DOScale(initialScale * collectibleAnimationSettings.ScaleBy, collectibleAnimationSettings.AnimationDuration / 2f)
                .OnComplete(() => myTransform.DOScale(initialScale, collectibleAnimationSettings.AnimationDuration / 2f)));

            sequence.SetLoops(-1);
            sequence.SetEase(collectibleAnimationSettings.AnimationEase);
        }
    }
}

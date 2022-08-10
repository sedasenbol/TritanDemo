using DG.Tweening;
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
                .DOScale(initialScale * collectibleImageAnimationSettings.ScaleBy,
                    collectibleImageAnimationSettings.TotalAnimationDuration / 2f).OnComplete(() =>
                    myTransform.DOScale(initialScale, collectibleImageAnimationSettings.TotalAnimationDuration / 2f));

            tween.SetEase(Ease.InOutSine);
        }
    }
}
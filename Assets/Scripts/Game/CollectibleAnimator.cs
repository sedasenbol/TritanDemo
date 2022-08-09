using System;
using DG.Tweening;
using GameCore;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game
{
    public class CollectibleAnimator : MonoBehaviour
    {
        private Sequence sequence;
        private Transform myTransform;

        private void OnEnable()
        {
            LevelManager.OnNewLevelLoaded += OnNewLevelLoaded;
            LevelManager.OnLevelCompleted += OnLevelCompleted;
            LevelManager.OnLevelFailed += OnLevelFailed;
        }
        
        private void OnDisable()
        {
            LevelManager.OnNewLevelLoaded -= OnNewLevelLoaded;
            LevelManager.OnLevelCompleted -= OnLevelCompleted;
            LevelManager.OnLevelFailed -= OnLevelFailed;
        }

        private void OnNewLevelLoaded()
        {
            myTransform = transform;

            sequence = DOTween.Sequence();

            
            sequence.Append(myTransform.DOBlendableRotateBy(new Vector3(0f, 360f, 0f), 3f, RotateMode.WorldAxisAdd));

            var initialPos = myTransform.position;
            
            sequence.Join(myTransform.DOMove(initialPos + new Vector3(0f, 1f, 0f), 1.5f).OnComplete(() =>
                 myTransform.DOMove(initialPos, 1.5f)));

            var initialScale = myTransform.lossyScale;

            sequence.Join(myTransform.DOScale(initialScale * 1.5f, 1.5f).OnComplete(() =>
                myTransform.DOScale(initialScale, 1.5f)));

            sequence.SetLoops(-1);
        }

        private void OnLevelCompleted() 
        {
            sequence.Kill();

            myTransform = null;
        }

        private void OnLevelFailed()
        {
            sequence.Kill();
            
            myTransform = null;
        }
    }
}

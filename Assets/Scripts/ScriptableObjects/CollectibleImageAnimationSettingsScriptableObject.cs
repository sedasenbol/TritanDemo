using DG.Tweening;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "CollectibleImageAnimationSettings", menuName = "ScriptableObjects/CollectibleImageAnimationSettings", order = 1)]
    public class CollectibleImageAnimationSettingsScriptableObject : ScriptableObject
    {
        [SerializeField] private float scaleBy = 1.25f;
        [SerializeField] private float totalAnimationDuration = 1f;
        [SerializeField] private Ease animationEase = Ease.InOutSine;

        public float ScaleBy => scaleBy;
        public float TotalAnimationDuration => totalAnimationDuration;
        public Ease AnimationEase => animationEase;
    }
}
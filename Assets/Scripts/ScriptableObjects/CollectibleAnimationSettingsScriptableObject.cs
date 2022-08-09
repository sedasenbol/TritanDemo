using DG.Tweening;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "CollectibleAnimationSettings", menuName = "ScriptableObjects/CollectibleAnimationSettings", order = 1)]
    public class CollectibleAnimationSettingsScriptableObject : ScriptableObject
    {
        [SerializeField] private Vector3 rotateBy = new Vector3(0f, 360f, 0f);
        [SerializeField] private float animationDuration = 3f;
        [SerializeField] private Vector3 moveBy = Vector3.up;
        [SerializeField] private float scaleBy = 1.5f;
        [SerializeField] private Ease animationEase = Ease.InOutSine;
        
        public Vector3 RotateBy => rotateBy;
        public float AnimationDuration => animationDuration;
        public Vector3 MoveBy => moveBy;
        public float ScaleBy => scaleBy;
        public Ease AnimationEase => animationEase;
    }
}
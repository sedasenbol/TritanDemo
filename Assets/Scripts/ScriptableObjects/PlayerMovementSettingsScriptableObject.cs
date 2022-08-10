using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "PlayerMovementSettings", menuName = "ScriptableObjects/PlayerMovementSettings", order = 1)]
    public class PlayerMovementSettingsScriptableObject : ScriptableObject
    {
        [SerializeField] private float rotationChangeDuration = 2f;

        public float RotationChangeDuration => rotationChangeDuration;
    }
}
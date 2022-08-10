using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "PlayerMovementSettings", menuName = "ScriptableObjects/PlayerMovementSettings", order = 1)]
    public class PlayerMovementSettingsScriptableObject : ScriptableObject
    {
        [SerializeField] private float speedChangeAmount = 0.5f;
        [SerializeField] private float maxSpeed = 20f;
        [SerializeField] private float minSpeed = 5f;

        public float SpeedChangeAmount => speedChangeAmount;
        public float MaxSpeed => maxSpeed;
        public float MinSpeed => minSpeed;
    }
}
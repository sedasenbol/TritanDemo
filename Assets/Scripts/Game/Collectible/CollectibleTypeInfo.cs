using UnityEngine;

namespace Game.Collectible
{
    public class CollectibleTypeInfo : MonoBehaviour
    {
        [SerializeField] private CollectibleType type;

        public CollectibleType Type => type;
    }
}
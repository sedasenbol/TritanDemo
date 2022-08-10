using UI;
using UnityEngine;

namespace Game.Collectible
{
    public class CollectibleCounter : Singleton<CollectibleCounter>
    {
        private int totalCollectibleCount;
        private int collectedCollectibleCount;

        public int CollectedCollectibleCount
        {
            get => collectedCollectibleCount;
            set
            {
                collectedCollectibleCount = value; 
                Debug.Log(collectedCollectibleCount);
                UIManager.Instance.UpdateCollectibleCounterText(collectedCollectibleCount, totalCollectibleCount);
            }
        }

        public int TotalCollectibleCount
        {
            get => totalCollectibleCount;
            set => totalCollectibleCount = value;
        }
    }
}
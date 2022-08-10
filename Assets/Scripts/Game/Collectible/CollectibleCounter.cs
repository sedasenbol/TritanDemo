using GameCore;
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
                UIManager.Instance.UpdateCollectibleCounterText(collectedCollectibleCount, totalCollectibleCount);

                if (collectedCollectibleCount != totalCollectibleCount) {return;}
                
                LevelManager.Instance.CompleteLevel();
            }
        }

        public int TotalCollectibleCount
        {
            get => totalCollectibleCount;
            set
            {
                collectedCollectibleCount = 0;
                totalCollectibleCount = value;
                UIManager.Instance.UpdateCollectibleCounterText(collectedCollectibleCount, totalCollectibleCount);
            }
        }
    }
}
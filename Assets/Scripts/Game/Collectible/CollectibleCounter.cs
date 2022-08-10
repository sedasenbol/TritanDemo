using UI;
using UnityEngine;

namespace Game.Collectible
{
    public class CollectibleCounter : Singleton<CollectibleCounter>
    {
        private int totalCollectibleCount;
        private int collectedCollectibleCount;

        public int CollectibleCount
        {
            get => collectedCollectibleCount;
            set
            {
                collectedCollectibleCount = value; 
                Debug.Log(collectedCollectibleCount);
                //UIManager
            }
        }

        public int TotalCollectibleCount
        {
            get => totalCollectibleCount;
            set
            {
                totalCollectibleCount = value;
                Debug.Log(totalCollectibleCount);
            }
        }
    }
}
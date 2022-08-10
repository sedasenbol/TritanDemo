using System;
using Pool;
using UnityEngine;

namespace Game.Collectible
{
    public class CollectibleRecycler : Singleton<CollectibleRecycler>
    {
        public void RecycleGameObject(GameObject go)
        {
            var type = go.GetComponent<CollectibleTypeInfo>().Type;
                
            switch (type)
            {
                case CollectibleType.Type1:
                    Collectible1Pool.Instance.RecycleGameObject(go);
                    break;
                case CollectibleType.Type2:
                    Collectible2Pool.Instance.RecycleGameObject(go);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
using System;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;

namespace Pool
{
    public class Collectible1Pool : Pool
    {
        private static Collectible1Pool instance;
        public static Collectible1Pool Instance
        {
            get
            {
                if (instance != null) return instance;
            
                instance = FindObjectOfType<Collectible1Pool>();

                if (instance != null) return instance;
            
                GameObject newGo = new GameObject();
                instance = newGo.AddComponent<Collectible1Pool>();
                return instance;
            }
        }

        protected void Awake()
        {
            instance = this as Collectible1Pool;
        }
    }
}
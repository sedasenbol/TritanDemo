using System;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;

namespace Pool
{
    public class Collectible2Pool : Pool
    {
        private static Collectible2Pool instance;
        public static Collectible2Pool Instance
        {
            get
            {
                if (instance != null) return instance;
            
                instance = FindObjectOfType<Collectible2Pool>();

                if (instance != null) return instance;
            
                GameObject newGo = new GameObject();
                instance = newGo.AddComponent<Collectible2Pool>();
                return instance;
            }
        }

        protected void Awake()
        {
            instance = this as Collectible2Pool;
        }
    }
}
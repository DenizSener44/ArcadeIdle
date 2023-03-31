using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SideSystems
{
    [Serializable]
    public class HealthSystem
    {
        [SerializeField] private int health;
        

        public int GetHealth() => health;

        public void SetHealth(int val) => health = val;

        public void SpendHealth(int val = 1)
        {
            health -= val;
            if (health <= 0) health = 0;
        }




    }
}

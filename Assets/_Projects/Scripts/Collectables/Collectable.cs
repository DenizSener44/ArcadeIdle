using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Collectables
{
    public class Collectable : MonoBehaviour
    {
        public CollectableData data;
        public void Collected()
        {
            GetComponent<Collider>().enabled = false;
        }
        
        
        
        
    }
}

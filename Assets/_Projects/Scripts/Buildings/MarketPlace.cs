using System.Collections;
using System.Collections.Generic;
using Collectables;
using DG.Tweening;
using Tools;
using UnityEngine;

namespace Buildings
{
    public class MarketPlace : MonoBehaviour
    {
        public float interactionStartDuration;
        public float interactionCompleteAmount;

        [SerializeField] private List<CollectableData> price = new List<CollectableData>();
        [SerializeField] private Sword swordPrefab;
        
        [SerializeField] private float minInstantiateRange = 2;
        [SerializeField] private float maxInstantiateRange = 3;

        public bool CanContinueInteraction(List<Collectable> collectables, out Collectable collectable)
        {
            if (!swordPrefab)
            {
                collectable = null;
                return false;
            }
            if (price.Count <1)
            {
                collectable = null;
                ObjectBought();
                return false;
            }
            
            for (int i = collectables.Count-1; i >= 0; i--)
            {
                if (price.Contains(collectables[i].data))
                {
                    collectable = collectables[i];
                    return true;
                }
            }
            
            
            collectable = null;
            return false;
        }

        


        public void CompleteInteraction(Collectable collectable)
        {
            price.Remove(collectable.data);
            collectable.transform.SetParent(transform);
            collectable.transform.DOLocalJump(Vector3.zero, 0, 1, 0.5f).OnComplete(ReduceCollectable);
        }

        private void ReduceCollectable()
        {
            
        }
        
        private void ObjectBought()
        {
            Vector3 sphere = Random.insideUnitSphere * Random.Range(minInstantiateRange,maxInstantiateRange);
            sphere.y = 0;
            Instantiate(swordPrefab, transform.position + sphere, Quaternion.identity);
            swordPrefab = null;
        }
        
    }
    
}
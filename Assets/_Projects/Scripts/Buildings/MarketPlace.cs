using System;
using System.Collections;
using System.Collections.Generic;
using Collectables;
using DG.Tweening;
using Tools;
using UI;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

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

        [SerializeField] private RectTransform marketPlaceCanvas;
        [SerializeField] private MarketPlaceItem itemPrefab;


        private Dictionary<CollectableData, int> _priceList = new FlexibleDictionary<CollectableData, int>();

        [SerializeField] private List<MarketPlaceItem> items = new List<MarketPlaceItem>();

        private void Start()
        {
            foreach (CollectableData c in price)
            {
                if (_priceList.ContainsKey(c))
                {
                    _priceList[c] += 1;
                }
                else
                {
                    _priceList.Add(c,1);
                }
            }

            CreateUI();
        }

        


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
            if (_priceList.ContainsKey(collectable.data))
            {
                _priceList[collectable.data] -= 1;
                if (_priceList[collectable.data] <= 0)
                {
                    _priceList.Remove(collectable.data);
                }
            }
            collectable.transform.SetParent(transform);
            collectable.transform.DOLocalJump(Vector3.zero, 0, 1, 0.5f).OnComplete(ReduceCollectable);
        }

        private void ReduceCollectable()
        {
            for (int i = items.Count-1; i >= 0; i--)
            {
                Destroy(items[i].gameObject);
            }
            items.Clear();
            CreateUI();
        }
        
        private void CreateUI()
        {
            foreach (CollectableData data in _priceList.Keys)
            {
                MarketPlaceItem m = Instantiate(itemPrefab, marketPlaceCanvas);
                m.Set(_priceList[data].ToString(),data.sprite);
                items.Add(m);
            }
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
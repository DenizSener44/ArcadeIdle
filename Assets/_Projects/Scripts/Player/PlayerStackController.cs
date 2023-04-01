using System;
using System.Collections;
using System.Collections.Generic;
using Buildings;
using Collectables;
using DG.Tweening;
using UnityEngine;

namespace Player
{
    public class PlayerStackController : MonoBehaviour
    {
        [SerializeField] private int playerMaxCarryAmount;
        [SerializeField] private int playerCurrentCarryAmount;
        [SerializeField] private Transform stackParent;
        [SerializeField] private float distanceBetweenStackedObjects = 0.5f;
        [SerializeField] private float collectableJumpDuration;
        [SerializeField]private List<Collectable> stack = new List<Collectable>();

        private Coroutine _marketPlaceRoutine;

        private void OnTriggerEnter(Collider other)
        {
            Collectable c = other.GetComponent<Collectable>();
            if (c)
            {
                TryStack(c);
                return;
            }

            MarketPlace m = other.GetComponent<MarketPlace>();
            if (m)
            {
                EnterMarketPlace(m);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            MarketPlace m = other.GetComponent<MarketPlace>();
            if (m && _marketPlaceRoutine != null)
            {
                StopCoroutine(_marketPlaceRoutine);
                _marketPlaceRoutine = null;
            }
        }


        private void StackCollectable(Collectable c)
        {
            stack.Add(c);
            c.Collected();
            c.transform.SetParent(stackParent);
            c.transform.DOLocalJump(Vector3.up*(distanceBetweenStackedObjects*stack.Count),
                2,1,collectableJumpDuration);
        }
        private void TryStack(Collectable c)
        {
            int newCarryAmount = playerCurrentCarryAmount + c.data.loadAmount;
            if(newCarryAmount > playerMaxCarryAmount) return;
            StackCollectable(c);
            playerCurrentCarryAmount = newCarryAmount;
        }
        
        
        
        private void EnterMarketPlace(MarketPlace marketPlace)
        {
            _marketPlaceRoutine = StartCoroutine(MarketPlaceRoutine(marketPlace));
        }

        private IEnumerator MarketPlaceRoutine(MarketPlace marketPlace)
        {
            yield return new WaitForSeconds(marketPlace.interactionStartDuration);
            while (marketPlace.CanContinueInteraction(stack, out Collectable c))
            {
                yield return new WaitForSeconds(marketPlace.interactionCompleteAmount);
                stack.Remove(c);
                marketPlace.CompleteInteraction(c);
                ReArrangeStack();
            }

            _marketPlaceRoutine = null;
        }

        private void ReArrangeStack()
        {
            for (int i = 0; i < stack.Count; i++)
            {
                stack[i].transform.DOLocalMove(Vector3.up * (distanceBetweenStackedObjects * i), 0.1f);
            }
        }
    }
}

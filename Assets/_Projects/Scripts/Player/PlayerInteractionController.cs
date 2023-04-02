using System;
using System.Collections;
using System.Collections.Generic;
using InteractionSystem;
using UnityEngine;

namespace Player
{
    public class PlayerInteractionController : MonoBehaviour
    {
        [SerializeField] private AnimationController animationController;
        [SerializeField] private float interactionspeed;
        
        private Interactable _interactable;
        private Coroutine _interactionRoutine;
        
        private void OnTriggerEnter(Collider other)
        {
            Interactable interactable = other.GetComponent<Interactable>();
            if (interactable)
            {
                _interactable = interactable;
                _interactionRoutine = StartCoroutine(InteractionRoutine());
            }
            
            
        }

        private void OnTriggerExit(Collider other)
        {
            Interactable interactable = other.GetComponent<Interactable>();
            if (interactable && interactable == _interactable)
            {
                _interactable.StopInteraction();
                _interactable = null;
                if(_interactionRoutine != null) StopCoroutine(_interactionRoutine);
            }
            
        }
        
        
        private IEnumerator InteractionRoutine()
        {
            yield return new WaitForSeconds(_interactable.interactionStartDuration);
            while (_interactable.CanContinueInteraction())
            {
                animationController.Attack();
                _interactable.StartInteraction(interactionspeed);
                yield return new WaitForSeconds(_interactable.interactionAnimationDuration);
                _interactable.CompleteInteraction();
            }
        }
    }

}
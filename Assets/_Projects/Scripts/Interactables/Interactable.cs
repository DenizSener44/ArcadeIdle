using System.Collections;
using System.Collections.Generic;
using Collectables;
using SideSystems;
using UI;
using UnityEngine;

namespace InteractionSystem
{
    public abstract class Interactable : MonoBehaviour
    {
        public float interactionStartDuration;
        public float interactionCompleteAmount;

        [SerializeField] protected UIFillBarController fillBar;
        [SerializeField] protected HealthSystem health;
        [SerializeField] private Collectable collectable;
        [SerializeField] private float minInstantiateRange = 2;
        [SerializeField] private float maxInstantiateRange = 3;
        protected virtual void OnEnable()
        {
            fillBar.maxValue = interactionCompleteAmount;
            fillBar.InitializeSlider();
            
        }
        
        

        public virtual bool CanContinueInteraction()
        {
            return health.GetHealth() > 0;
        }

        public virtual void StartInteraction(float interactionspeed)
        {
            fillBar.StartFillingSlider(interactionspeed);
        }

        public virtual void CompleteInteraction()
        {
            fillBar.StopSlider();
            fillBar.InitializeSlider();
            health.SpendHealth();
            CreateCollectable();
        }

        private void CreateCollectable()
        {
            Vector3 sphere = Random.insideUnitSphere * Random.Range(minInstantiateRange,maxInstantiateRange);
            sphere.y = 0;
            Instantiate(collectable, transform.position + sphere, Quaternion.identity);
        }

        public virtual void StopInteraction()
        {
            fillBar.StopSlider();
            fillBar.InitializeSlider(); 
        }
    }
}

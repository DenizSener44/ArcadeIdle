using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIFillBarController : MonoBehaviour
    {
        public float minValue = 0;
        public float maxValue = 100;
        public float currentValue;

        [SerializeField] private Slider slider;
     
        private Coroutine _fillUIRoutine;


        public void InitializeSlider()
        {
            slider.minValue = minValue;
            slider.maxValue = maxValue;
            currentValue = minValue;
            slider.value = currentValue;
        }



        public void StartFillingSlider(float speed)
        {
            if(_fillUIRoutine != null) return;
            _fillUIRoutine = StartCoroutine(FillUIRoutine(speed));
        }

        private IEnumerator FillUIRoutine(float speed)
        {
            while (currentValue < maxValue)
            {
                currentValue += speed * Time.deltaTime;
                slider.value = currentValue;
                yield return null;
            }
            slider.value = minValue;
            _fillUIRoutine = null;
        }


        private void SetValue(float newVal)
        {
            slider.value = newVal;
        }

        public void StopSlider()
        {
            if(_fillUIRoutine == null) return;
            StopCoroutine(_fillUIRoutine);
            _fillUIRoutine = null;
            SetValue(minValue);
        }

        
    }

}
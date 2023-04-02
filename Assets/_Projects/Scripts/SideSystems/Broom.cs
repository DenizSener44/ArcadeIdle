using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace SideSystems
{
    
    public class Broom : MonoBehaviour
    {
        [SerializeField] private Transform rotator;
        private void Start()
        {
            rotator.DOLocalRotate(new Vector3(0, 0, 15), 1f).SetLoops(-1, LoopType.Yoyo);
        }
    }

    
}
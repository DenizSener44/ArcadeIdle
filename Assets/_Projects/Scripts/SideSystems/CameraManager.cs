using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

namespace SideSystems
{
    public class CameraManager : MonoBehaviour,ICameraShaker
    {
        [SerializeField] private CinemachineVirtualCamera gameCam;
        [SerializeField] private CameraShakeData[] cameraShakeDatas;

        private CinemachineBasicMultiChannelPerlin _cmp;
        private Coroutine _shakeCamRoutine;

        private void Start()
        {
            _cmp = gameCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            
        }




        public void ShakeCam(CameraShakeType t)
        {
            if(_shakeCamRoutine != null)return;
            foreach (CameraShakeData data in cameraShakeDatas)
            {
                if (data.type != t) continue;
                _shakeCamRoutine = StartCoroutine(ShakeCamRoutine(data));
                return;
            }
            
        }

        private IEnumerator ShakeCamRoutine(CameraShakeData t)
        {
            float timer = t.duration;
            _cmp.m_AmplitudeGain = t.intensity;

            while (timer > 0)
            {
                _cmp.m_AmplitudeGain = t.intensity * timer / t.duration;
                timer -= Time.deltaTime;
                yield return null;
            }

            _cmp.m_AmplitudeGain = 0;
            _shakeCamRoutine = null;
        }
        
        
        
        
    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SideSystems
{
    public class AudioManager : MonoBehaviour,IAudioPlayer
    {
        [SerializeField] private AudioData[] audioDatas;
        [SerializeField] private AudioSource source;
        
        
        public void PlayAudio(AudioType type)
        {
            foreach (AudioData data in audioDatas)
            {
                if (data.type != type) continue;
                source.clip = data.clip;
                source.Play();
                return;
            }
        }
    }

}
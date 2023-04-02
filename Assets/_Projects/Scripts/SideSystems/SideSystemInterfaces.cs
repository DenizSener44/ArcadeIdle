using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;




public interface IAudioPlayer
{
    public void PlayAudio(AudioType type);
}

public enum AudioType
{
    Stone,
    Wood,
    Broom,
    Death
}
[Serializable]
public struct AudioData
{
    public AudioType type;
    public AudioClip clip;
}


public interface ICameraShaker
{
    public void ShakeCam(CameraShakeType t);
}

public interface ICameraChanger
{
    public void OpenCam(CameraType type);
    public void CloseCam();
}

public enum CameraType
{
    Stone,
    Wood,
    Sword,
    Enemy,
    Broom
}
[Serializable]
public struct CameraChangeData
{
    public CameraType type;
    public CinemachineVirtualCamera camera;
}



public enum CameraShakeType
{
    VeryMild,
    Mild,
    Hard,
    
}

[Serializable]
public struct CameraShakeData
{
    public CameraShakeType type;
    public float duration;
    public float intensity;
}




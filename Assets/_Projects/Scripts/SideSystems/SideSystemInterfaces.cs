using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;



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




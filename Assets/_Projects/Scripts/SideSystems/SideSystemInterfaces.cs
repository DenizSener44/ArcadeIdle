using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public interface ICameraShaker
{
    public void ShakeCam(CameraShakeType t);
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




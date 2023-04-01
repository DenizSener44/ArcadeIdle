using System;
using Tools;
using UnityEngine;


public interface ISwordOpener
{
    public Action OnSwordOpen { get; set; }
}


[Serializable]
public struct States
{
    public StateNames currentState;
    public AnimatorOverrideController overrideController;
    public Tool tool;
        
}

public enum StateNames
{
    Idle,
    Sword,
    Pickaxe,
    Axe
}
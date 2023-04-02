using System;
using Tools;
using UnityEngine;


public interface ISwordOpener
{
    public Action OnSwordOpen { get; set; }
}

public interface IPlayerDeathController
{
    public Action OnPlayerDead{ get; set; }
}

public interface IPlayerKillCounter
{
    public bool PlayerKilledEnemy{ get; set; }
}

public interface IPlayerStackCounter
{
    public int StoneCount { get; set; }
    public int WoodCount { get; set; }
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
    Axe,
    Broom
}
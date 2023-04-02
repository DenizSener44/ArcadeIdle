using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface ITutorialChanger
{
    public void OpenTutorial(TutorialType type);
    public void CloseTutorial();

}

[Serializable]
public struct TutorialChangeData
{
    public TutorialType type;
    public string text;
}

public enum TutorialType
{
    Stone,
    Wood,
    Sword,
    Enemy,
    Broom
}


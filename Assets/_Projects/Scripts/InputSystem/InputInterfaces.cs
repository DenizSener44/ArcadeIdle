using System;
using UnityEngine;

public interface IInputData
{
    public Action<Vector2> OnInputUpdate { get; set; }
    public Action OnInputStarted { get; set; }
    public Action OnInputReleased { get; set; }
}

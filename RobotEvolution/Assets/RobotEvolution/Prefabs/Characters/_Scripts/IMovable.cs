using UnityEngine;

public interface IMovable 
{
    public Vector3 CurrentdirectionView { get; }
    public Vector3 CurrentdirectionMove { get; }
    public float CurrentSpeedMovement { get; }
    public float CurrentSpeedRotation { get; }
    public bool IsMovableCharacter { get; }
}

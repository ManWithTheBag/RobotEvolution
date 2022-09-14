using UnityEngine;

public interface IMovable 
{
    public Transform Turret { get; }
    public Quaternion CurrentBodyView { get; }
    public Quaternion CurrentTurretView { get; }
    public Vector3 CurrentCharacterMove { get; }
    public float CurrentSpeedMovement { get; }
    public CharacterModelStatsDataSO CharacterModelStatsDataSO { get; }
    public bool IsMovableCharacter { get; }
}

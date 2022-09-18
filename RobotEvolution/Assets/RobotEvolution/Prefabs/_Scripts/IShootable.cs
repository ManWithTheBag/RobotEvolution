using UnityEngine;

public interface IShootable
{
    public float ViewAngleTurretAndVeapon { get; }
    public float MaxShootDistance { get;}
    public void TryToShoot(Transform enemyTransform);
    public bool IsEnemleThisComponent { get; }
}

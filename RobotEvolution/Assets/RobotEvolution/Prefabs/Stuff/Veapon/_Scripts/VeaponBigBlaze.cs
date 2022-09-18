using UnityEngine;

public class VeaponBigBlaze : AbsVeaponRayCast
{
    public override void SetupThisVeapon()
    {
        ViewAngleTurretAndVeapon = _veaponDataSO.ViewAngleTurretAndVeaponBigBlaze;
        MaxShootDistance = _veaponDataSO.MaxDistanceBigBlaze;
        _timeRechargeVeapon = _veaponDataSO.TimeRechargeBigBlaze;
    }

    public override void ChangeScore(Transform enemyTransform)
    {
        if(enemyTransform.TryGetComponent(out IDetectable enemyDetectable))
        {
            enemyDetectable.DetectedLossScore(_veaponDataSO.ScoreDamageBigBlaze);

            _thisTransform.TryGetComponent(out IDetectable thisIDetectable);
            thisIDetectable.DetectedAddScore(_veaponDataSO.ScoreDamageBigBlaze);
        }
    }
}

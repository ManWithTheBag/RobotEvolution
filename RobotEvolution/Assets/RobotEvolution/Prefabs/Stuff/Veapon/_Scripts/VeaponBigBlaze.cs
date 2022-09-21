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
        if(enemyTransform.TryGetComponent(out CharacterChangeScore characterChangeScore))
        {
            characterChangeScore.ScoreChangedLossScore(_veaponDataSO.ScoreDamageBigBlaze);

            _thisTransform.TryGetComponent(out ScoreCalculation scoreCalculation);
            scoreCalculation.AddScore((int)(_veaponDataSO.ScoreDamageBigBlaze / _veaponDataSO.ReductionGetScoreFactor));
        }

        if (enemyTransform.TryGetComponent(out ShieldDetected shieldDetected))
            shieldDetected.InvokeShieldDetectedEvent(_veaponDataSO.ScoreDamageBigBlaze);
    }
}
